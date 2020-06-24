namespace Reshape.Common.EventBus
{
    /// <summary>
    /// Enum describing the various states an event can go through.
    /// They should be fairly self-descriptive.
    /// </summary>
    public enum EventStateEnum
    {
        NotPublished = 0,
        InProgress = 1,
        Published = 2,
        PublishedFailed = 3
    }
}
