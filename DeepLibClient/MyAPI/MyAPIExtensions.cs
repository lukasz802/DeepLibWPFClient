﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace DeepLibClient
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for MyAPI.
    /// </summary>
    public static partial class MyAPIExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<Creator> GetCreators(this IMyAPI operations)
            {
                return Task.Factory.StartNew(s => ((IMyAPI)s).GetCreatorsAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<Creator>> GetCreatorsAsync(this IMyAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetCreatorsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='creator'>
            /// </param>
            public static Creator PostCreator(this IMyAPI operations, Creator creator = default(Creator))
            {
                return Task.Factory.StartNew(s => ((IMyAPI)s).PostCreatorAsync(creator), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='creator'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Creator> PostCreatorAsync(this IMyAPI operations, Creator creator = default(Creator), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostCreatorWithHttpMessagesAsync(creator, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='creatorId'>
            /// </param>
            public static Creator GetCreator(this IMyAPI operations, int creatorId)
            {
                return Task.Factory.StartNew(s => ((IMyAPI)s).GetCreatorAsync(creatorId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='creatorId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Creator> GetCreatorAsync(this IMyAPI operations, int creatorId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetCreatorWithHttpMessagesAsync(creatorId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='creatorId'>
            /// </param>
            /// <param name='creator'>
            /// </param>
            public static void PutCreator(this IMyAPI operations, int creatorId, Creator creator = default(Creator))
            {
                Task.Factory.StartNew(s => ((IMyAPI)s).PutCreatorAsync(creatorId, creator), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='creatorId'>
            /// </param>
            /// <param name='creator'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PutCreatorAsync(this IMyAPI operations, int creatorId, Creator creator = default(Creator), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PutCreatorWithHttpMessagesAsync(creatorId, creator, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='creatorId'>
            /// </param>
            public static void DeleteCreatorItem(this IMyAPI operations, int creatorId)
            {
                Task.Factory.StartNew(s => ((IMyAPI)s).DeleteCreatorItemAsync(creatorId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='creatorId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteCreatorItemAsync(this IMyAPI operations, int creatorId, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DeleteCreatorItemWithHttpMessagesAsync(creatorId, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<MediaElement> GetMediaElements(this IMyAPI operations)
            {
                return Task.Factory.StartNew(s => ((IMyAPI)s).GetMediaElementsAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<MediaElement>> GetMediaElementsAsync(this IMyAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetMediaElementsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='mediaElement'>
            /// </param>
            public static MediaElement PostMediaElement(this IMyAPI operations, MediaElement mediaElement = default(MediaElement))
            {
                return Task.Factory.StartNew(s => ((IMyAPI)s).PostMediaElementAsync(mediaElement), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='mediaElement'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MediaElement> PostMediaElementAsync(this IMyAPI operations, MediaElement mediaElement = default(MediaElement), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostMediaElementWithHttpMessagesAsync(mediaElement, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='mediaElementId'>
            /// </param>
            public static MediaElement GetMediaElement(this IMyAPI operations, int mediaElementId)
            {
                return Task.Factory.StartNew(s => ((IMyAPI)s).GetMediaElementAsync(mediaElementId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='mediaElementId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MediaElement> GetMediaElementAsync(this IMyAPI operations, int mediaElementId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetMediaElementWithHttpMessagesAsync(mediaElementId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='mediaElementId'>
            /// </param>
            /// <param name='mediaElement'>
            /// </param>
            public static void PutMediaElement(this IMyAPI operations, int mediaElementId, MediaElement mediaElement = default(MediaElement))
            {
                Task.Factory.StartNew(s => ((IMyAPI)s).PutMediaElementAsync(mediaElementId, mediaElement), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='mediaElementId'>
            /// </param>
            /// <param name='mediaElement'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PutMediaElementAsync(this IMyAPI operations, int mediaElementId, MediaElement mediaElement = default(MediaElement), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PutMediaElementWithHttpMessagesAsync(mediaElementId, mediaElement, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='mediaElementId'>
            /// </param>
            public static void DeleteMediaElement(this IMyAPI operations, int mediaElementId)
            {
                Task.Factory.StartNew(s => ((IMyAPI)s).DeleteMediaElementAsync(mediaElementId), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='mediaElementId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteMediaElementAsync(this IMyAPI operations, int mediaElementId, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DeleteMediaElementWithHttpMessagesAsync(mediaElementId, null, cancellationToken).ConfigureAwait(false);
            }

    }
}
