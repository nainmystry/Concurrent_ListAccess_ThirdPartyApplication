public class ThirdPartyApp
{
    private static List<string> _propA = new List<string>();
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1,1);

    static ThirdPartyApp()
    {
        Task.Run(async () => {
            Random random = new Random();
            int indx = 0;
            while (true)
            {
                await _semaphore.WaitAsync();
                try
                {
                    _propA.Add(indx.ToString());
                    if (_propA.Count > 50)
                    {
                        break; 
                    }
                    indx++;
                }
                finally
                {
                    _semaphore.Release();
                }
                await Task.Delay(100);
                //considering some other processing.
            }
        });
    }

    public static async Task<List<string>> GetPropAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            return new List<string>(_propA);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}