# MayBeKnuckles
## A collection of experiments for Valve's Knuckles EV3s
Documention? What's that? I haven't slept and it's 6AM.

## Discord Server
Want to talk? See updates? Join this project's Discord server: https://discord.gg/N3fEC2f

### Requirements
Unity 2018
SteamVR Unity Plugin v2.0.1 - [link](https://github.com/ValveSoftware/steamvr_unity_plugin/releases)

### KnuckleTyping
Prototype keyboard for the Knuckles. It uses your pointer and middle finger as well as two action keys pressed by your thumbs. You need to set up a 'typing' action set with "Key1" and "Key2" bound to something. Then map it to the KnuckleTyping script. Key 1 on the left hand is for the first key of a cord and key 2 of the right hand is for the last key of a cord. Hold down the key to type an uppercase letter.

The current keymap for it is trash and should be upgraded to a more QWERTY like order.

### KnucklePoses
[![](https://i.imgur.com/8wModys.gif)](https://i.imgur.com/8wModys.gifv "Animated")

This script allows you to easily detect when the user makes certain poses with their fingers. Finger guns, flipping the bird, spider-man web shooting. It's all possible. Try it tonight! Or... today? I need sleep.

#### The example doesn't work out of the box. You have to go into the [CameraRig] within MayBePlayer and change each of the controllers to use their respective Pose Action.

![](https://i.imgur.com/g8Zpc9g.png)
