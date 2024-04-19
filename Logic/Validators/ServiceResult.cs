namespace Logic.Validators
{
    public class ServiceResult
    {
        private readonly List<string> _Unresolved = [];
        public void AddError(string error)
        {
            _Unresolved.Add(error);
        }
        public bool IsSuccess()
        {
            if (_Unresolved.Count > 0)
            {
                AutoExceptionThrow();
            }
            return _Unresolved.Count == 0;
        }
        private void AutoExceptionThrow()
        {
            throw new Exception(_Unresolved[0]);
        }
    }
}
