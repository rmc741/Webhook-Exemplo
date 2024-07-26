namespace Webhook.API
{
    public record Subscription(string Topic, string Callback);

    public record PublishRequest(string Topic, object Message);

    public class WebhookService
    {
    }
}
