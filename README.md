# Head Pose Estimation

A simple project to detect the pose landmark of human and it application.
---

## 🧰 Tools and Libraries Used

| Category | Name / Version | Purpose |
|-----------|----------------|----------|
| Engine | Unity **2022.3.62f2** | Main game engine |
| IDE | Rider, VSCode | C#/MediaPipe |
| Version Control | Git/GitHub | Source management |
| Packages | MediaPipeUnity,  | MediaPipe integrated in Unity |
| External SDKs | Mediapipe | AI |
| Other Tools | Blender | Asset creation and design |

---

## 🧩 Development Approach

### 1. **Research**
- Since I do have some exprericens with AI, so for me, the biggest problem I think is how to connect the AI with the Unity. After some research, there was 2 approach that seem both possible and easy to code.
  - **Approach 1:** Find/create an Unity package that embeded right into Unity. After research and testing, the best possible package are [MediaPipeUnityPlugin](https://github.com/homuler/MediaPipeUnityPlugin).
  - **Approach 2:** Setup a simple UDP connect running locally. Where the Pose Landmarks run on seperate application (acting as a lightweight server), which send real time data to Unity via UDP. Then the Unity will processing the logic.

| **Approach** | **Pros** | **Cons** |
|---------------|-----------|-----------|
| **Approach 1** (Integrated Model in Unity) | - Simple to deploy, can be built directly into the `.exe`.<br>- Easier to develop, since everyting in the same Unity project. | - The plugin documentation notes that in some cases it may call deprecated APIs, potentially causing crashes.<br> - The current plugin does have option to run the Pose Landmark using GPU|
| **Approach 2** (External Python Server via UDP) | - Allows flexibility by running AI logic externally (Python, MediaPipe).<br>- Have better performance. | - Requires managing two separate applications (Unity client + Python server).<br> - Hard to deloy, but this can be solve using Docker. |


### 2. **Code**
- Pose Landmark Detection scene is the main scene for the assignment.
- There are 2 main script to handle the task.
  - Pose Landmarker Runner: The mauin script to detect the pose landmark. Using multiple thread the lower the workload of the cpu. Invoke the event for other script handle the logic
  - Head Rotation Controller: Reive the landmark result to handle the logic, in this case is to rotate the camera corresoponding to the head.

---

## Features


| Feature | Description |
|----------|--------------|
| Pose landmark | Using base MediaPipe pose Landmark to detect 33 points of the human position  |
| Camera  Sync | Camera sync with the rotation of the head |
| UI/UX | Just a simple UI to show the webcam |

---

## Result

Currently I only do the first approach. Right below is the result, I also add 2 demo video 


<video src="https://raw.githubusercontent.com/Malgodev/HeadPoseEstimationInUnity/master/close.mp4" 
       width="640" height="360" controls>
Your browser does not support the video tag.
</video>


| Component | Specification |
|------------|----------------|
| Result Frame Rate | 70 FPS |
| Device(s) Used | PC (RTX 4060), 32GB RAM, R5 5500|
| Other Tools | Unity Profiler |

Enviroments: [Link](https://www.blenderkit.com/asset-gallery-detail/a9267e7e-7cd7-4d5d-b479-8bf8fff2eddf/)
