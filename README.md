# AuraTween
A lightweight, simple, and high performance tweening library for Unity.

## Reason

I wanted a modern, simple, small, performant, expandable, and mostly unopinionated library to perform tweening actions in Unity.

## Performance Comparison

I picked some popular tweening libraries to compare performance with while developing.

These benchmarks were run on my computer (Ryzen 7 5800X3D, Windows 11 Pro Build 22621), so results may vary.

This active tween test spawns `N` number of tweens at once that last for 0.5 seconds using the `OutQuad` easing.
Each tween moves its own empty GameObject from `Vector3.zero` to `Vector3.one`. This measures the average frame timings across that 0.5 second period.

NOTE: If the library had an option for pre-allocating the number of tweens, it was used before the times were measured.

|                                                                          | 1,000 Tweens | 2,000 Tweens | 5,000 Tweens | 10,000 Tweens | 100,000 Tweens |
|--------------------------------------------------------------------------|--------------|--------------|--------------|---------------|----------------|
| AuraTween                                                                | 1.05ms       | 1.25ms       | 1.78ms       | 2.65ms        | 28.58ms        |
| [DOTween](http://dotween.demigiant.com)                                  | 1.07ms       | 1.29ms       | 1.95ms       | 2.97ms        | 36.66ms        |
| [LeanTween](https://openupm.com/packages/com.oss.leantween)              | 1.10ms       | 1.31ms       | 1.95ms       | 3.08ms        | 35.33ms        |
| [nl.elracoone.tweens](https://openupm.com/packages/nl.elraccoone.tweens) | 1.17ms       | 1.51ms       | 2.40ms       | 3.85ms        | 43.88ms        |
| [AnimeTask](https://openupm.com/packages/dev.kyubuns.animetask)          | 1.44ms       | 2.01ms       | 3.65ms       | 6.33ms        | 110.82ms       |

## Differences Between Other Libraries

The main difference between this library and almost every other tweening library is its simplicity.
There are only a few extension methods and built type handlers included.
This allows you to build off the library and tune the functionality according to your needs.

## Installation

*Currently requires Unity 2021.3+, older Unity version support can be implemented on request.*

### OpenUPM
Install with [OpenUPM](https://openupm.com) (recommended)
```
openupm add dev.auros.auratween
```

### Git URL
```
https://github.com/Auros/AuraTween.git?path=/AuraTween/Assets/AuraTween
```

## Usage

In your component, add a serialized field for `TweenManager` and assign it in the editor.

You can call `.Run(...)` on it. Six extension methods are available for the types `float`, `Vector2`, `Vector3`, `Quaternion`, `Pose`, and `Color`.
There is also another extension method that allows a generic type.
```cs
public class MyBehaviour : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    
    [SerializeField]
    private float _duration = 2f;
    
    [SerializeField]
    private TweenManager _tweenManager;
    
    [SerializeField]
    private AnimationCurve _animationCurve;

    private void Start()
    {
        var myTransform = transform;
        var myMaterial = _renderer.material;

        // float
        _tweenManager.Run(-5f, 5f, _duration, value => Debug.Log($"float: ${value}"), Easer.OutCubic, this);
        
        // Vector3
        var tween = _tweenManager.Run(Vector3.zero, new Vector3(0f, 5f, 0f), _duration, value => myTransform.localPosition = value, Easer.InOutExpo, this);
    
        // Set cancellation event
        tween.SetOnCancel(() => Debug.Log("Tween Canceled!"));
        
        // Set completion event
        tween.SetOnComplete(() => Debug.Log("Tween Completed!"));
    
        // Pause a tween
        tween.Pause();
    
        // Cancel a tween
        tween.Cancel();
        
        // Custom types and or interpolators
        _tweenManager.Run(Color.red, Color.cyan, _duration, value => myMaterial.color = value, Easer.OutElastic, HSV, this);
        
        // Custom easing procedures
        _tweenManager.Run(Quaternion.identity, Quaternion.Euler(new Vector3(0f, 90f, 0f)), _duration, value => myTransform.localRotation = value, CustomEaseWithAnimationCurve, this);
    }
    
    private static Color HSV(ref Color start, ref Color end, ref float time)
    {
        Color.RGBToHSV(start, out var startH, out var startS, out var startV);
        Color.RGBToHSV(end, out var endH, out var endS, out var endV);
        var h = Mathf.Lerp(startH, endH, time);
        var s = Mathf.Lerp(startS, endS, time);
        var v = Mathf.Lerp(startV, endV, time);
        return Color.HSVToRGB(h, s, v);
    }
    
    private float CustomEaseWithAnimationCurve(ref float time)
    {
        return _animationCurve.Evaluate(time);
    }
}
```

The `.Run(...)` extension methods ONLY accept the `EaseProcedure` delegate for easing options, so any method that has a `ref float` for its parameter and returns a `float`.
A similar rule follows for the custom type/value calculation system (called interpolators). This is what keeps the library simple and makes it very expandable.

The `Easer` class contains procedure methods for all of the common easing functions (`OutQuad`, `InOutSine`, `InCirc`, `OutBounce`, etc.).
To make it easier to serialize the different types of easings, there is an `Ease` enum with an extension method `.ToProcedure()`.


```cs
public class MyBehaviour : MonoBehaviour
{
    [SerializeField]
    private Ease _ease;
    
    [SerializeField]
    private TweenManager _tweenManager;
    
    private void Start()
    {
        _tweenManager.Run(-5f, 5f, 1f, value => Debug.Log($"float: ${value}"), _ease.ToProcedure(), this);
    }
}
```

### UniTask Support

If [UniTask](https://github.com/Cysharp/UniTask) is detected, you can await the tweens.

```cs
public class MyBehaviour : MonoBehaviour
{
    [SerializeField]
    private TweenManager _tweenManager;
    
    private async UniTaskVoid Start()
    {
        await _tweenManager.Run(-5f, 5f, 1f, value => Debug.Log($"float: ${value}"), Easer.InCubic, this);
        
        var myTween = _tweenManager.Run(-5f, 5f, 1f, value => Debug.Log($"float: ${value}"), Easer.InCubic, this);
        await myTween;
    }
}
```

## Note

You're expected to build your own extension methods and tooling to fit your own project's needs.
If you're looking for an all-in-one, component-based, quick prototype maker tweening library and or you're not well versed with C#, this library may not be for you.

## Credits

[Caeden117](https://github.com/Caeden117) and [PlusOneRabbit](https://github.com/PlusOneRabbit) for helping with optimizing `Vector3` calculations.
