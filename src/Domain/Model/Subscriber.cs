﻿using Domain.ApplicationErrors;
using Domain.Invariants;
using Domain.Invariants.Extensions;
using Domain.Model.Base;
using Domain.Results;

namespace Domain.Model;

public class Subscriber : Entity
{
    public string ExternalIdentifier { get; }

    private List<Subscription> _subscriptions;
    public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions;

    public IReadOnlyCollection<Media> SubscribedToMedia => _subscriptions
        .Select(subscription => subscription.Media)
        .ToList();

    private Subscriber(Guid id, string externalIdentifier) : base(id)
    {
        if (string.IsNullOrEmpty(externalIdentifier))
            throw new ArgumentException($"{nameof(externalIdentifier)} must be set");

        ExternalIdentifier = externalIdentifier;
        _subscriptions = new List<Subscription>();
    }

    public static Result<Subscriber> Create(Guid id, string externalIdentifier)
    {
        return Invariant.Create
            .NotNullOrWhiteSpace(externalIdentifier, nameof(externalIdentifier))
            .ValidateAndCreate(() => new Subscriber(id, externalIdentifier));
    }

    public void Subscribe(Media media)
    {
        var subscription = Subscription.Create(Guid.NewGuid(), media, Id);
        if (!SubscribedToMedia.Contains(media))
            _subscriptions.Add(subscription);
    }

    public Result Unsubscribe(Media media)
    {
        var subscription = _subscriptions.SingleOrDefault(subscription => media.Equals(subscription.Media));
        return Result.SuccessIf(
            subscription == null || _subscriptions.Remove(subscription),
            Errors.Subscriber.UnsubscribeFailedError(media.Name)
        );
    }
}