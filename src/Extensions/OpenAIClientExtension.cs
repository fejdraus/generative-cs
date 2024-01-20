using ChatAIze.GenerativeCS.Clients;
using ChatAIze.GenerativeCS.Options.OpenAI;
using Microsoft.Extensions.DependencyInjection;

namespace ChatAIze.GenerativeCS.Extensions;

public static class OpenAIExtension
{
    public static IServiceCollection AddOpenAIClient(this IServiceCollection services, Action<OpenAIClientOptions>? options = null)
    {
        if (options != null)
        {
            _ = services.Configure(options);
        }

        _ = services.AddHttpClient<OpenAIClient>();
        _ = services.AddSingleton<OpenAIClient>();

        return services;
    }

    public static IServiceCollection AddOpenAIClient(this IServiceCollection services, string apiKey)
    {
        return services.AddOpenAIClient(o =>
        {
            o.ApiKey = apiKey;
        });
    }

    public static IServiceCollection AddOpenAIClient(this IServiceCollection services, string apiKey, ChatCompletionOptions? defaultCompletionOptions)
    {
        return services.AddOpenAIClient(o =>
        {
            o.ApiKey = apiKey;

            if (defaultCompletionOptions != null)
            {
                o.DefaultCompletionOptions = defaultCompletionOptions;
            }
        });
    }

    public static IServiceCollection AddOpenAIClient(this IServiceCollection services, string apiKey, EmbeddingOptions? defaultEmbeddingOptions)
    {
        return services.AddOpenAIClient(o =>
        {
            o.ApiKey = apiKey;

            if (defaultEmbeddingOptions != null)
            {
                o.DefaultEmbeddingOptions = defaultEmbeddingOptions;
            }
        });
    }

    public static IServiceCollection AddOpenAIClient(this IServiceCollection services, string apiKey, TextToSpeechOptions? defaultTextToSpeechOptions)
    {
        return services.AddOpenAIClient(o =>
        {
            o.ApiKey = apiKey;

            if (defaultTextToSpeechOptions != null)
            {
                o.DefaultTextToSpeechOptions = defaultTextToSpeechOptions;
            }
        });
    }

    public static IServiceCollection AddOpenAIClient(this IServiceCollection services, string apiKey, TranscriptionOptions? defaultTranscriptionOptions)
    {
        return services.AddOpenAIClient(o =>
        {
            o.ApiKey = apiKey;

            if (defaultTranscriptionOptions != null)
            {
                o.DefaultTranscriptionOptions = defaultTranscriptionOptions;
            }
        });
    }

    public static IServiceCollection AddOpenAIClient(this IServiceCollection services, string apiKey, TranslationOptions? defaultTranslationOptions)
    {
        return services.AddOpenAIClient(o =>
        {
            o.ApiKey = apiKey;

            if (defaultTranslationOptions != null)
            {
                o.DefaultTranslationOptions = defaultTranslationOptions;
            }
        });
    }
}
