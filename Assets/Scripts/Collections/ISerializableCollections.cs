
using UnityEngine;

namespace MChojniak.Collections
{
    public interface ISerializableCollections : ISerializationCallbackReceiver {}

    public interface ISerializableDictionary : ISerializableCollections {}

    public interface ISerializableHashSet : ISerializableCollections {}

    public interface ISerializableQueue : ISerializableCollections {}
    
    public interface ISerializableStack : ISerializableCollections {}
}   