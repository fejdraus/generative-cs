using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using GenerativeCS.Models;
using GenerativeCS.Options;
using GenerativeCS.Services.OpenAI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GenerativeCS.Providers;

public class ChatGPT
{
    private readonly HttpClient _client = new();

    public ChatGPT() { }

    [SetsRequiredMembers]
    public ChatGPT(string apiKey)
    {
        ApiKey = apiKey;
    }

    [SetsRequiredMembers]
    [ActivatorUtilitiesConstructor]
    public ChatGPT(IOptions<ChatGPTOptions> options)
    {
        ApiKey = options.Value.ApiKey;
    }

    public required string ApiKey
    {
        get => _client.DefaultRequestHeaders.Authorization?.Parameter!;
        set => _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value);
    }

    public async Task<string> CompleteAsync(string prompt, ChatGPTCompletionOptions? options = null, CancellationToken cancellationToken = default)
    {
        return await CompleteAsync(new ChatConversation(prompt), options, cancellationToken);
    }

    public async Task<string> CompleteAsync(ChatConversation conversation, ChatGPTCompletionOptions? options = null, CancellationToken cancellationToken = default)
    {
        return await ChatCompletions.CompleteAsync(conversation, ApiKey, _client, options, cancellationToken);
    }

    public async IAsyncEnumerable<string> CompleteAsStreamAsync(ChatConversation conversation, ChatGPTCompletionOptions? options = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var chunk in ChatCompletions.CompleteAsStreamAsync(conversation, ApiKey, _client, options, cancellationToken))
        {
            yield return chunk;
        }
    }

    public async Task<List<float>> GetEmbeddingAsync(string text, ChatGPTEmbeddingOptions? options = null, CancellationToken cancellationToken = default)
    {
        return await Embeddings.GetEmbeddingAsync(text, ApiKey, _client, options, cancellationToken);
    }
}
