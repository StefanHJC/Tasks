public class Example
{
    private bool _isEnabled;

    public void SetEnabled() => _isEnabled = true;

    public void SetDisabled() => _isEnabled = false;

    private void Update()
    {
        if (_isEnabled)
            _effects.StartEnableAnimation();
        else
            _pool.Free(this);
    }
}