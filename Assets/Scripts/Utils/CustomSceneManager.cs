using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public sealed class CustomSceneManager : NetworkSceneManagerBase
{
    [SerializeField] private List<AssetReference> _scenes = new List<AssetReference>();

    [Header("Single Peer Options")] public int PostLoadDelayFrames = 1;

    private bool GetAddressableSceneIndex(int index, out int sceneAssetIndex)
    {
        bool isValid = index >= 0 && index <= _scenes.Count;

        sceneAssetIndex = isValid && index != 0 ? index - 1 : 0;

        return isValid;
    }

    
    public IEnumerator LoadSceneAsync(SceneRef sceneRef, LoadSceneParameters parameters,
        Action<Scene> loaded)
    {
        if (!GetAddressableSceneIndex(sceneRef, out var sceneIndex))
        {
            throw new InvalidOperationException($"Not going to load {sceneRef}: unable to find the scene");
        }

        if (_scenes[sceneIndex].IsValid())
        {
            Debug.Log("Scene already loaded - unloading!");
            // In order to load a scene again, we need to first unload it, however
            // we can't simply unload the current scene because that would leave us with zero scenes loaded, so we load a dummy replacement scene instead.
            // If you load additively and have a "base" that is always loaded, you can simply unload _scenes[sceneIndex]
            yield return SceneManager.LoadSceneAsync(1);
            Debug.Log("Scene unloaded!");
        }

        AsyncOperationHandle<SceneInstance> op = _scenes[sceneIndex].LoadSceneAsync(parameters.loadSceneMode);

Debug.Log(op.PercentComplete);
 
        yield return new WaitUntil(() => op.IsDone);

        loaded(op.Result.Scene);
    }


  
    private YieldInstruction UnloadSceneAsync(Scene scene)
    {
        return SceneManager.UnloadSceneAsync(scene);
    }

    protected override IEnumerator SwitchScene(SceneRef prevScene, SceneRef newScene, FinishedLoadingDelegate finished)
    {
        if (Runner.Config.PeerMode == NetworkProjectConfig.PeerModes.Single)
        {
          
            return SwitchSceneSinglePeer(prevScene, newScene, finished);
        }
        else
        {
            return SwitchSceneMultiplePeer(prevScene, newScene, finished);
        }
    }

    private IEnumerator SwitchSceneMultiplePeer(SceneRef prevScene, SceneRef newScene,
        FinishedLoadingDelegate finished)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        bool canTakeOverActiveScene = prevScene == default && IsScenePathOrNameEqual(activeScene, newScene);

        LogTrace($"Start loading scene {newScene} in multi peer mode");
        var loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Additive,
            NetworkProjectConfig.ConvertPhysicsMode(Runner.Config.PhysicsEngine));

        var sceneToUnload = Runner.MultiplePeerUnityScene;
        var tempSceneSpawnedPrefabs =
            Runner.IsMultiplePeerSceneTemp ? sceneToUnload.GetRootGameObjects() : Array.Empty<GameObject>();

        if (canTakeOverActiveScene && NetworkRunner.GetRunnerForScene(activeScene) == null &&
            SceneManager.sceneCount > 1)
        {
            LogTrace("Going to attempt to unload the initial scene as it needs a separate Physics stage");
            yield return UnloadSceneAsync(activeScene);
        }

        if (SceneManager.sceneCount == 1 && tempSceneSpawnedPrefabs.Length == 0)
        {
            // can load non-additively, stuff will simply get unloaded
            LogTrace($"Only one scene remained, going to load non-additively");
            loadSceneParameters.loadSceneMode = LoadSceneMode.Single;
        }
        else if (sceneToUnload.IsValid())
        {
            // need a new temp scene here; otherwise calls to PhysicsStage will fail
            if (Runner.TryMultiplePeerAssignTempScene())
            {
                LogTrace($"Unloading previous scene: {sceneToUnload}, temp scene created");
                yield return UnloadSceneAsync(sceneToUnload);
            }
        }

        LogTrace($"Loading scene {newScene} with parameters: {JsonUtility.ToJson(loadSceneParameters)}");

        Scene loadedScene = default;
        yield return LoadSceneAsync(newScene, loadSceneParameters, scene => loadedScene = scene);

        LogTrace($"Loaded scene {newScene} with parameters: {JsonUtility.ToJson(loadSceneParameters)}: {loadedScene}");

        if (!loadedScene.IsValid())
        {
            throw new InvalidOperationException($"Failed to load scene {newScene}: async op failed");
        }

        var sceneObjects = FindNetworkObjects(loadedScene, disable: true, addVisibilityNodes: true);

        // unload temp scene
        var tempScene = Runner.MultiplePeerUnityScene;
        Runner.MultiplePeerUnityScene = loadedScene;
        if (tempScene.IsValid())
        {
            if (tempSceneSpawnedPrefabs.Length > 0)
            {
                LogTrace(
                    $"Temp scene has {tempSceneSpawnedPrefabs.Length} spawned prefabs, need to move them to the loaded scene.");
                foreach (var go in tempSceneSpawnedPrefabs)
                {
                    Assert.Check(go.GetComponent<NetworkObject>(),
                        $"Expected {nameof(NetworkObject)} on a GameObject spawned on the temp scene {tempScene.name}");
                    SceneManager.MoveGameObjectToScene(go, loadedScene);
                }
            }

            LogTrace($"Unloading temp scene {tempScene}");
            yield return UnloadSceneAsync(tempScene);
        }

        finished(sceneObjects);
    }

    private IEnumerator SwitchSceneSinglePeer(SceneRef prevScene, SceneRef newScene,
        FinishedLoadingDelegate finished)
    {
        Scene loadedScene;
        Scene activeScene = SceneManager.GetActiveScene();

        bool canTakeOverActiveScene = prevScene == default && IsScenePathOrNameEqual(activeScene, newScene);

        if (canTakeOverActiveScene)
        {
            LogTrace($"Not going to load initial scene {newScene} as this is the currently active scene");
            loadedScene = activeScene;
        }
        else
        {
            LogTrace($"Start loading scene {newScene} in single peer mode");
            LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);

            loadedScene = default;
            LogTrace($"Loading scene {newScene} with parameters: {JsonUtility.ToJson(loadSceneParameters)}");

            
            yield return LoadSceneAsync(newScene, loadSceneParameters, scene => loadedScene = scene);
            

            LogTrace(
                $"Loaded scene {newScene} with parameters: {JsonUtility.ToJson(loadSceneParameters)}: {loadedScene}");

            if (!loadedScene.IsValid())
            {
                throw new InvalidOperationException($"Failed to load scene {newScene}: async op failed");
            }
        }

        for (int i = PostLoadDelayFrames; i > 0; --i)
        {
            yield return null;
        }

        var sceneObjects = FindNetworkObjects(loadedScene, disable: true);
        finished(sceneObjects);
    }
}