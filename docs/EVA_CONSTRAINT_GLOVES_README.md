# EVA-Inspired Constraint Gloves and Helmet Simulation

A reader’s guide for building a low-cost, repeatable glove-and-helmet simulation for a mixed-reality space-maintenance study using the Meta Quest 3.

This guide is intended for research teams building a **space-maintenance constraint costume**: a lightweight prototype that makes hand-based tasks feel more like suited work without claiming to replicate a real pressurized EVA suit.

---

## Table of Contents

1. [Purpose](#purpose)
2. [Design Principle](#design-principle)
3. [What the Glove Should Simulate](#what-the-glove-should-simulate)
4. [Recommended Glove Build](#recommended-glove-build)
5. [Meta Quest 3 Tracking Considerations](#meta-quest-3-tracking-considerations)
6. [Helmet Simulation](#helmet-simulation)
7. [Recommended Combined Setup](#recommended-combined-setup)
8. [Build Checklist](#build-checklist)
9. [Pilot Calibration](#pilot-calibration)
10. [Study Integration Notes](#study-integration-notes)
11. [Safety Notes](#safety-notes)
12. [Suggested Paper Wording](#suggested-paper-wording)

---

## Purpose

The goal is to create a research-ready physical setup for an AR/VR study on instruction placement during simulated space-maintenance tasks.

The prototype should be:

- **Cheap** enough to build quickly.
- **Repeatable** across participants.
- **Safe** for short experimental sessions.
- **Trackable** by the Quest 3 or by external visual markers.
- **Constraining** enough to affect dexterity, touch, and wrist mobility.
- **Believable** enough to support the space-maintenance framing.

The glove and helmet simulation are not only props. They help create the embodied constraints under which participants experience the AR interfaces.

---

## Design Principle

Do **not** try to build a real pressurized glove or a sealed astronaut helmet.

Instead, build an **EVA-inspired constraint glove** and a **helmet-like mixed-reality experience**.

A defensible study framing is:

> A low-cost EVA-inspired constraint glove designed to simulate the bulk, reduced tactile feedback, and movement restrictions of space-maintenance handwear.

This is better than claiming the prototype is a realistic astronaut glove. The prototype approximates selected constraints relevant to hand-intensive maintenance work.

---

## What the Glove Should Simulate

The glove should introduce four main constraints:

| Constraint | Design Goal |
|---|---|
| Bulky fingers | Make small components harder to grasp and press. |
| Reduced fingertip tactility | Make it harder to feel wires, buttons, debris, and knobs. |
| Reduced finger bending | Make flexing the fingers slightly effortful. |
| Restricted wrist movement | Make the hand feel attached to a suit-like cuff. |

The glove should make tasks slower or more effortful, but not impossible.

---

## Recommended Glove Build

### Overview

Build the glove as layered components:

```text
[ inner hygiene glove ]
        +
[ white/gray work glove ]
        +
[ fingertip bulkers ]
        +
[ finger-bending resistance ]
        +
[ wrist/forearm cuff ]
        +
[ tracking markers or visual features ]
```

---

### Layer 1: Inner Hygiene and Comfort Layer

Use one of the following:

| Material | Purpose |
|---|---|
| Thin cotton glove | Comfort and sweat absorption. |
| Nitrile glove | Hygiene barrier between users and outer glove. |
| Optional fingerless liner | Extra comfort for longer sessions. |

Change or sanitize the inner layer between participants.

---

### Layer 2: Base Glove

Use a **white or gray mechanic glove**, motorcycle glove, gardening glove, or light work glove.

Recommended qualities:

| Feature | Why It Helps |
|---|---|
| Slightly padded back of hand | Looks technical and suit-like. |
| Flexible palm | Keeps the tasks feasible. |
| Velcro wrist strap | Allows quick fitting across participants. |
| Light color | Supports the astronaut/suit aesthetic. |
| Visible seams or panels | Helps optical hand tracking distinguish finger shape. |

Avoid fully black gloves if relying on Quest hand tracking. Dark, featureless gloves may reduce tracking reliability.

---

### Layer 3: Fingertip Bulk and Tactile Reduction

This is the most important layer.

Add removable fingertip caps over each finger. Prioritize the **thumb**, **index finger**, and **middle finger**, because these are used most often for pinching and fine manipulation.

Options:

| Version | Materials | Notes |
|---|---|---|
| Soft | EVA foam wrapped around fingertips | Easiest to build and adjust. |
| Medium | Silicone thimbles or rubber finger protectors | Good balance of bulk and durability. |
| Hard | 3D-printed rounded fingertip shells with foam inside | More controlled but slower to fabricate. |

The caps should make it harder to:

- Press small buttons.
- Grab thin wires.
- Feel surfaces.
- Pick up small objects.
- Distinguish small shapes by touch.

Expected task effects:

| Task | Expected Glove Effect |
|---|---|
| Wire routing | Harder to pinch and insert wires. |
| Valve alignment | Harder to grip and rotate valves. |
| Filter sorting | Harder to distinguish and place debris pieces. |
| Circuit calibration | Harder to press small buttons or turn small knobs. |

---

### Layer 4: Finger-Bending Resistance

Add light resistance to finger flexion.

Good options:

| Method | How to Implement |
|---|---|
| Elastic bands | Run elastic from the fingertips to the back of the hand. |
| Foam strips | Add foam over finger joints to resist bending. |
| Thin plastic strips | Place flexible strips along the dorsal side of fingers. |
| Velcro tension straps | Add adjustable resistance for each participant. |

Recommended option: **elastic bands on the back of the fingers**.

They make flexing feel more effortful while still allowing participants to open their hands quickly.

Do **not** use anything that locks the fingers or makes it hard to release objects.

---

### Layer 5: Wrist Restriction and Forearm Cuff

Add a forearm cuff that looks like a suit interface and slightly restricts wrist movement.

Materials:

| Material | Purpose |
|---|---|
| Neoprene wrist brace | Base support and wrist restriction. |
| EVA foam cylinder or cuff | Adds astronaut-like bulk. |
| Velcro straps | Allows fitting and adjustment. |
| White duct tape or fabric tape | Creates a suit-like surface. |

Suggested layout:

```text
[ bulky glove ] -- [ wrist ring / cuff ] -- [ forearm display zone ]
```

Add a rectangular **forearm display zone** using white or light-gray material. This becomes the visual anchor for the forearm-anchored AR condition.

For the forearm-anchored condition, place a visual marker, fiducial marker, AprilTag, or high-contrast patch on this zone.

---

### Exact Recommended Glove Prototype

For the first research prototype, build this version:

| Part | Recommendation |
|---|---|
| Base | White/gray mechanic glove. |
| Fingertips | EVA foam caps or silicone finger protectors. |
| Palm | Rubber grip pads. |
| Finger resistance | Elastic bands from fingertips to the back of the hand. |
| Wrist | Neoprene wrist brace under an EVA foam cuff. |
| Forearm | White foam or fabric sleeve with rectangular AR anchor patch. |
| Tracking markers | High-contrast fingertip patches or small colored dots. |

Build two variants:

| Version | Use |
|---|---|
| Light glove | Pilot testing, debugging, and task development. |
| Constraint glove | Final experiment sessions. |

---

## Meta Quest 3 Tracking Considerations

The glove should be designed for both realism and trackability.

Potential problems and fixes:

| Problem | Fix |
|---|---|
| Quest cannot see finger joints clearly | Add dark seam lines over knuckles. |
| Fingers merge visually | Add contrast bands between fingers. |
| Fingertips are not detected | Add small colored or white fingertip patches. |
| Glove is too bulky | Track the controller, wrist, or forearm marker instead of each finger. |
| Hand-proximal AR jitters | Anchor cues to task objects or a wrist marker, not only to the hand skeleton. |

Recommended tracking strategy per condition:

| AR Condition | Tracking Strategy |
|---|---|
| World-anchored AR | Anchor to physical task board markers. |
| Forearm-anchored AR | Anchor to wrist/forearm marker or controller. |
| Hand-proximal AR | Use hand tracking if stable; otherwise anchor near the hand using a wrist marker. |
| Tablet baseline | No AR tracking needed. |

Do not depend entirely on precise fingertip tracking unless pilot tests show it is stable with the glove.

---

## Helmet Simulation

Do not build a sealed helmet around the Quest 3.

Instead, create the feeling of a helmet through:

1. Visual overlays.
2. Audio ambience.
3. A lightweight physical collar.

---

### Layer A: Visual Helmet Overlay in AR

In Unity, add a semi-transparent visor overlay that remains constant across all AR conditions.

Possible elements:

| Effect | Implementation |
|---|---|
| Rounded visor border | Transparent black/gray vignette at screen edges. |
| Slight tint | Very subtle amber, gray, or blue visor tint. |
| Helmet scratches | Low-opacity texture near the edges only. |
| HUD labels | Oxygen, comms, task timer, suit status. |
| Restricted field of view | Slight mask, but not too aggressive. |
| Reflections | Very faint curved visor reflection. |

Example HUD:

```text
O2: 94%
COMMS: LOCAL
TASK: MAINTENANCE PANEL A
SUIT TEMP: NOMINAL
STEP 1/3
```

Keep the overlay subtle. The paper is about instruction placement, so the helmet overlay should not compete with task instructions.

---

### Layer B: Audio Helmet Simulation

Use low-volume audio to make the headset feel more like a helmet.

| Audio Cue | Intended Effect |
|---|---|
| Low breathing loop | Helmet presence. |
| Soft ventilation or fan noise | Suit atmosphere. |
| Radio click before instructions | Mission-control feeling. |
| Slight muffling or low-pass filter | Enclosed helmet feeling. |
| Occasional short comms beep | Space-operation atmosphere. |

Keep volume low. The audio should support the context without becoming a confound.

Example start message before each trial:

> Suit comms online. Maintenance task ready. Proceed when prepared.

---

### Layer C: Physical Helmet Collar

Build a shoulder or neck ring that sits on the participant’s shoulders. It should not attach to or cover the Quest 3.

Materials:

| Part | Material |
|---|---|
| Neck ring | EVA foam, cardboard, or lightweight plastic. |
| Shoulder yoke | Foam board or craft foam. |
| White cover | White fabric, duct tape, or vinyl. |
| Details | Fake screws, warning labels, or suit patch. |

Suggested structure:

```text
       [ Quest 3 headset ]
          open air
     -------------------
     soft helmet collar
     shoulder EVA ring
     -------------------
```

The collar can slightly restrict head movement, but it should not cause discomfort or interfere with headset ventilation, cameras, straps, or tracking.

---

## Recommended Combined Setup

Use the same costume setup across all experimental conditions.

| Component | Purpose |
|---|---|
| Quest 3 | Passthrough AR. |
| EVA-inspired constraint glove | Hand difficulty. |
| Forearm cuff | AR anchor and suit realism. |
| Shoulder/neck ring | Helmet/suit feeling. |
| Helmet visor overlay | Visual immersion. |
| Breathing/comms audio | Helmet atmosphere. |

This keeps the space-maintenance context consistent. The only thing that changes across conditions should be the **instruction placement**.

---

## Build Checklist

### Glove Materials

| Item | Suggested Quantity |
|---|---:|
| White/gray mechanic gloves | 2–3 pairs |
| Thin cotton or nitrile inner gloves | Many |
| EVA foam sheets, 2–5 mm | 2–3 sheets |
| Neoprene wrist brace | 1–2 |
| Elastic cord or bands | 1 pack |
| Velcro strips | 1 pack |
| White/gray fabric tape | 1 roll |
| Silicone finger protectors or thimbles | 1 pack |
| Rubber grip pads | 1 pack |
| High-contrast stickers or markers | 1 pack |

### Helmet and Collar Materials

| Item | Suggested Quantity |
|---|---:|
| EVA foam floor mat or craft foam | 1–2 sheets |
| Foam board or cardboard | 1 sheet |
| White vinyl, fabric, or tape | 1 roll |
| Velcro straps | 1 pack |
| Small fake labels or warning stickers | Optional |
| Lightweight headphones or earbuds | 1 pair |

---

## Pilot Calibration

Before the full study, run a short glove calibration pilot.

### Suggested Calibration Tasks

| Micro-task | Target Outcome |
|---|---|
| Press 10 small buttons | Slower than bare hand, but still feasible. |
| Pick up 10 small debris pieces | Annoying but not impossible. |
| Rotate 3 knobs | Requires effort but does not cause strain. |
| Insert 4 wires | Produces occasional errors if careless. |

Compare bare-hand performance against glove performance.

A useful target is for the glove to increase task difficulty by around **20–40%**, not make tasks dramatically harder.

If tasks become too difficult, reduce constraint in this order:

1. Reduce fingertip cap thickness.
2. Reduce elastic resistance.
3. Loosen the wrist cuff.
4. Increase the size of buttons, wires, or knobs.

---

## Study Integration Notes

The glove and helmet simulation should remain constant across all interface conditions.

Recommended constant setup:

```text
Participant wears:
- Quest 3
- EVA-inspired constraint glove
- Forearm cuff
- Shoulder/neck collar
- Helmet visor overlay
- Low-volume suit audio
```

Experimental conditions can then vary only by instruction placement:

| Condition | Instruction Placement |
|---|---|
| World-anchored AR | Instructions attached to task objects or the maintenance board. |
| Forearm-anchored AR | Checklist anchored to the forearm cuff. |
| Hand-proximal AR | Tooltips and cues near the hand or current target. |
| Tablet/paper baseline | Instructions on a non-AR reference surface. |

This helps ensure that differences in workload, usability, or task performance are more likely caused by the instruction interface rather than by changes in the costume or environment.

---

## Safety Notes

Use this prototype only for short, supervised research sessions.

Before each session, check that:

- The glove does not lock the fingers.
- Participants can open their hand quickly.
- The wrist cuff does not cut circulation.
- The collar does not interfere with headset straps, vents, cameras, or tracking.
- The participant can see the physical task board clearly.
- The task area is free of tripping hazards.
- The audio volume is low and does not block verbal communication.
- The participant can stop immediately if uncomfortable.

Do not seal the Quest 3 inside a helmet shell or cover headset ventilation.

---

## Suggested Paper Wording

### Glove Description

> To approximate the embodied constraints of space-maintenance work, participants wore an EVA-inspired constraint glove consisting of a padded work glove, fingertip bulkers, elastic finger-flexion resistance, and a forearm cuff. The glove was not intended to replicate a pressurized EVA glove, but to introduce controlled reductions in dexterity, tactile feedback, and wrist mobility relevant to hand-intensive maintenance tasks.

### Helmet Description

> To contextualize the mixed-reality experience as helmet-mediated work, we added a constant semi-transparent visor overlay, suit-status HUD, low-volume breathing and ventilation audio, and a lightweight shoulder collar. These elements remained constant across conditions.

### Calibration Description

> We calibrated the glove to introduce moderate manual constraint while preserving task feasibility.

---

## Notes for Future Iterations

Possible future improvements:

- Add IMU-based wrist tracking.
- Add AprilTags or ArUco markers to the forearm display zone.
- Create interchangeable fingertip cap thicknesses.
- Compare light, medium, and heavy glove constraint levels.
- Add physiological measures such as grip force or forearm exertion.
- Add participant comfort ratings after each condition block.

---

## License / Use

This guide is intended as a research prototyping document. Adapt the build details to your available materials, local safety requirements, and institutional ethics review process.
