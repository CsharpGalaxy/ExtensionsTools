
using AutoMapper;

namespace CsharpGalexy.LibraryExtention.Extentions.AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Extension methods for AutoMapper to support multi-source mapping, merging, cloning, and dynamic configurations.
/// </summary>
public static class AutoMapperExtentions
{
    #region Multi-Source Mapping (2 or 3 Sources)

    /// <summary>
    /// Maps two source objects into a single destination object.
    /// First maps source1 to destination, then maps source2 onto the same destination.
    /// </summary>
    /// <typeparam name="TSource1">Type of the first source</typeparam>
    /// <typeparam name="TSource2">Type of the second source</typeparam>
    /// <typeparam name="TDestination">Type of the destination</typeparam>
    /// <param name="mapper">AutoMapper instance</param>
    /// <param name="source1">First source object</param>
    /// <param name="source2">Second source object</param>
    /// <returns>Mapped destination object</returns>
    public static TDestination Map<TSource1, TSource2, TDestination>(
        this IMapper mapper,
        TSource1 source1,
        TSource2 source2)
    {
        var destination = mapper.Map<TSource1, TDestination>(source1);
        return mapper.Map(source2, destination);
    }

    /// <summary>
    /// Maps three source objects into a single destination object.
    /// Maps source1 → destination, then source2 → destination, then source3 → destination.
    /// </summary>
    /// <typeparam name="TSource1">Type of the first source</typeparam>
    /// <typeparam name="TSource2">Type of the second source</typeparam>
    /// <typeparam name="TSource3">Type of the third source</typeparam>
    /// <typeparam name="TDestination">Type of the destination</typeparam>
    /// <param name="mapper">AutoMapper instance</param>
    /// <param name="source1">First source object</param>
    /// <param name="source2">Second source object</param>
    /// <param name="source3">Third source object</param>
    /// <returns>Mapped destination object</returns>
    public static TDestination Map<TSource1, TSource2, TSource3, TDestination>(
        this IMapper mapper,
        TSource1 source1,
        TSource2 source2,
        TSource3 source3)
    {
        var destination = mapper.Map<TSource1, TDestination>(source1);
        var destination2 = mapper.Map<TSource2, TDestination>(source2);
        return mapper.Map(source3, destination2);
    }

    #endregion

    #region Generic Merge (Object-Based)

    /// <summary>
    /// Merges two objects into a result of type TResult.
    /// First maps item1 to TResult, then maps item2 onto the result.
    /// Uses runtime mapping (object), so ensure AutoMapper configuration supports these types.
    /// </summary>
    /// <typeparam name="TResult">Destination type</typeparam>
    /// <param name="mapper">AutoMapper instance</param>
    /// <param name="item1">First object to map</param>
    /// <param name="item2">Second object to merge</param>
    /// <returns>Merged result</returns>
    public static TResult MergeInto<TResult>(
        this IMapper mapper,
        object item1,
        object item2)
    {
        return mapper.Map(item2, mapper.Map<TResult>(item1));
    }

    /// <summary>
    /// Merges multiple objects into a result of type TResult.
    /// Maps the first object to TResult, then sequentially merges each subsequent object onto the result.
    /// Uses runtime mapping (object), so ensure AutoMapper configuration supports these types.
    /// </summary>
    /// <typeparam name="TResult">Destination type</typeparam>
    /// <param name="mapper">AutoMapper instance</param>
    /// <param name="objects">Array of objects to merge</param>
    /// <returns>Merged result</returns>
    /// <exception cref="ArgumentException">Thrown if objects array is null or empty</exception>
    public static TResult MergeInto<TResult>(
        this IMapper mapper,
        params object[] objects)
    {
        if (objects == null || objects.Length == 0)
            throw new ArgumentException("At least one object must be provided for merging.", nameof(objects));

        var res = mapper.Map<TResult>(objects.First());
        return objects.Skip(1).Aggregate(res, (r, obj) => mapper.Map(obj, r));
    }

    #endregion

    #region Collection Mapping

 
    /// <summary>
    /// Maps a collection of source objects onto an existing list of destination objects.
    /// Updates existing items in-place based on index.
    /// If destination list is smaller, new items will be added. If larger, extra items remain untouched.
    /// </summary>
    /// <typeparam name="TSource">Source type</typeparam>
    /// <typeparam name="TDestination">Destination type</typeparam>
    /// <param name="mapper">AutoMapper instance</param>
    /// <param name="sourceList">Source collection</param>
    /// <param name="destinationList">Existing destination list to update</param>
    public static void MapToExistingList<TSource, TDestination>(
        this IMapper mapper,
        IEnumerable<TSource> sourceList,
        IList<TDestination> destinationList)
    {
        if (sourceList == null || destinationList == null) return;

        var sourceArray = sourceList.ToArray();
        for (int i = 0; i < sourceArray.Length; i++)
        {
            if (i < destinationList.Count)
                mapper.Map(sourceArray[i], destinationList[i]);
            else
                destinationList.Add(mapper.Map<TDestination>(sourceArray[i]));
        }
    }

    #endregion

    #region Dynamic Mapping & Configuration


  
    #endregion

    #region Validation & Utility


    /// <summary>
    /// Creates a deep clone of the source object using AutoMapper.
    /// Source and destination types must be the same or mappable.
    /// </summary>
    /// <typeparam name="T">Type to clone</typeparam>
    /// <param name="mapper">AutoMapper instance</param>
    /// <param name="source">Object to clone</param>
    /// <returns>Cloned object</returns>
    public static T Clone<T>(this IMapper mapper, T source)
    {
        if (source == null) return default;
        return mapper.Map<T>(source);
    }

    #endregion

}
