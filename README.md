# Introduction

Activate and swing the controllers to move.

## Setup

### Adding the package to the Unity project manifest

* Navigate to the `Packages` directory of your project.
* Adjust the [project manifest file][Project-Manifest] `manifest.json` in a text editor.
  * Ensure `https://registry.npmjs.org/` is part of `scopedRegistries`.
    * Ensure `io.extendreality` is part of `scopes`.
    * Ensure `com.fight4dream` is part of `scopes`.
  * Add `com.fight4dream.locomotors.moveinplace.unity` to `dependencies`, stating the latest version.

  A minimal example ends up looking like this. Please note that the version `X.Y.Z` stated here is to be replaced with [the latest released version][Latest-Release] which is currently [![Release][Version-Release]][Releases].
  ```json
  {
    "scopedRegistries": [
      {
        "name": "npmjs",
        "url": "https://registry.npmjs.org/",
        "scopes": [
          "io.extendreality",
          "com.fight4dream"
        ]
      }
    ],
    "dependencies": {
      "com.fight4dream.locomotors.moveinplace.unity": "X.Y.Z",
      ...
    }
  }
  ```
* Switch back to the Unity software and wait for it to finish importing the added package.

### Done

The `FIGHT4DREAM Locomotors MoveInPlace Unity` package will now be available in your Unity project `Packages` directory ready for use in your project.

The package will now also show up in the Unity Package Manager UI. From then on the package can be updated by selecting the package in the Unity Package Manager and clicking on the `Update` button or using the version selection UI.

[Unity]: https://unity3d.com/
[Unity Package Manager]: https://docs.unity3d.com/Manual/upm-ui.html
[Project-Manifest]: https://docs.unity3d.com/Manual/upm-manifestPrj.html
[Version-Release]: https://img.shields.io/github/package-json/v/fight4dream/Fight4Dream.Locomotors.MoveInPlace.Unity?label=release
[Releases]: ../../releases
[Latest-Release]: ../../releases/latest

## Tutorial

[VRTK V4 Move In Place][TutorialVideo]

[TutorialVideo]: https://youtu.be/nBmMOtwUCtQ

## License

Code released under the [MIT License][License].

[License]: LICENSE.md

## Credit

Thank Zack Cheng (zkkzkk32312) for extensive testing.
