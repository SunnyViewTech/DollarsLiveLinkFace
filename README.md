# Dollars Live Link Face

## Use Live Link Face in Unity Project

[![Watch the video](https://img.youtube.com/vi/ngy8vYPSrGk/0.jpg)](https://youtu.be/ngy8vYPSrGk)

## Pros:

- Easy to use compared with [Facial AR Remote](https://github.com/Unity-Technologies/facial-ar-remote) and [Unity Face Capture](https://docs.unity3d.com/Packages/com.unity.live-capture@latest)

- Usable after packaging

## Usage:

1. Add the LiveLinkFace prefab to the scene

![image](https://user-images.githubusercontent.com/123790383/216753570-bd273be9-efc0-468b-af7c-6c106fa2672a.png)

2. Create a new mapping asset

![image](https://user-images.githubusercontent.com/123790383/216753225-75c43d1c-2325-46fa-bb1a-e087ebdf8064.png)

3. Enter the names of blendshapes of the mesh

![image](https://user-images.githubusercontent.com/123790383/216753271-eae32dc5-9dad-4b22-b50d-2b1f36c44285.png)

4. Add the meshes and mapping assets to the Blend Shapes Controller component of the prefab, the length and order of the two arrays should be the same

![image](https://user-images.githubusercontent.com/123790383/216753542-7cd55098-7f56-4c19-be8c-a226ec830375.png)


## Note:

- If there is no corresponding blendshape in the mesh, the mapping can be left blank
- The mapping is case sensitive
- The last nine items in the mapping asset are ignored

![image](https://user-images.githubusercontent.com/123790383/216753614-9860fad3-d5b7-401d-a922-04c1ab66b110.png)

