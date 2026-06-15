## Stack
1. Unity - Game engine
2. Meta Horizon Link - Rapid development by running Unity project directly in HMD via cable
3. OpenXR - Popular XR standard and runtime
4. Meta Core SDK - Meta's XR development package designed for Quest devices

## Compatible Hardware
- Meta Quest 3
- Meta Quest 3s

## Unity Packages
1. XR Plugin Management
2. Meta All-In-One SDK
3. Meta MR Utility Kit (MRUK)
4. Meta XR Movement SDK

## [Meta Building Blocks](https://developers.meta.com/horizon/documentation/unity/unity-building-blocks-overview)
1. Camera Rig
2. Passthrough
3. Passthrough Camera Access

## Notes
- Need to set 'Project Settings -> Player -> Active Input Handling' to 'Both'
- Make sure you have Room Scale boundary setup in the Quest

## Assets
1. [Sci Fi Game UI collection FREE version](https://sungraphica.itch.io/sci-fi-game-ui-collection-free-version)
2. [free-sci-fi-game-icons](https://krukowski.itch.io/free-sci-fi-game-icons)
3. Font - [Sakana Font Regular](https://www.1001fonts.com/sakana-font.html)

## 🟢 World-anchored AR
Uses QR Code tracking capability of MRUK. MRUK provides pose and transform data which can be used to project 3D objects in AR.

### Possible alternatives:
- AprilTags
- Aruco Marker detection
    - difficult / limited documentation in unity

### Todo:
- make less shaky
- finish QRCode and QRCodeManager classes
- improve tracking when moving 
    - try ZXing approach with [QuestCameraKit](https://github.com/xrdevrob/QuestCameraKit#3--qr-code-tracking-with-zxing)
- add boolean for making face camera or not
- make marker kill itself if not tracked for (~5s maybe?)
- markers persist after condition change

## 🟠 Forearm-anchored AR
Uses Movement SDK to get forearm data (via Body_LeftHandWristTwist bone from OVRSkeleton). Pros over forearm estimation with hand-tracking: EVERYTHING. Cons: might be overkill. 

### Todo:
- make less shaky

## 🟣 Hand-proximal AR
Uses hand anchor provided by Meta SDK CameraRig building block, which provides position data for hands and even eyes. Apply some offset and it's good to go.

### Todo:
- make less shaky
- depends on rotation need to fix that

## 🟡 Tablet / Paper baseline - DONE
No need to dev.

## Other Todos
- improve space-themed panel (plan is to make it more Borderlands 2-y)
- generally make things less shaky
- add support for choosing dominant hand
- add support for different colors (theming)
- make system to switch conditions
