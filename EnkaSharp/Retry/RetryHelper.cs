namespace EnkaSharp.Retry;

public static class RetryHelper
{
    public static async Task ExecuteAsync(Func<Task> action, int attempts, TimeSpan delay)
    {
        for (var i = 0; i < attempts; i++)
        {
            try
            {
                await action();
                return;
            }
            catch when (i < attempts - 1)
            {
                await Task.Delay(delay);
            }
        }

        await action();
    }

    public static async Task<T> ExecuteAsync<T>(Func<Task<T>> action, int attempts, TimeSpan delay)
    {
        for (var i = 0; i < attempts; i++)
        {
            try
            {
                return await action();
            }
            catch when (i < attempts - 1)
            {
                await Task.Delay(delay);
            }
        }

        return await action();
    }
}