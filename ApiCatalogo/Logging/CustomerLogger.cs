namespace ApiCatalogo.Logging;


public class CustomerLogger : ILogger
{
    readonly string name;
    readonly CustomLoggerProviderConfiguration loggerconfig;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        this.name = name;
        loggerconfig = config;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerconfig.LogLevel;
    }
    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
        Exception exception, Func<TState, Exception, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoArquivoLog = @"C:\Users\richa\OneDrive\Documentos\workspace\aulas-c#\ApiCatalogo\dados\log\Richard_Log.txt";
       
        using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
        {
            try
            {
                streamWriter.WriteLine(mensagem);
                streamWriter.Flush();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
