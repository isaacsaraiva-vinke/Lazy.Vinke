
# Lazy.Vinke.Json
.Net Json Serializer & Deserializer

### Supported features
- #### Built-in Serializers\Deserializers
  - Type (Based on .Net Assemblies and Namespaces)
  - DataSet (Typed data table collection)
  - DataTable (Typed columns preserving row state and values)
  - DateTime (Customizable format [default: yyyy-MM-ddTHH:mm:ss:fffZ])
  - Generic Dictionary\<T\>
  - Generic Tuple\<T\>
  - Generic List\<T\>

- #### Facilities
  - ##### Globally define Serializer\Deserializer for types just once
    -> new JsonSerializerOptionsGlobal().Add\<YourTypeSerializer\>(typeof(DataTable));  \
    -> new JsonDeserializerOptionsGlobal().Add\<YourTypeDeserializer\>(typeof(DataTable));
  - ##### Support easily property ignore by adding attribute to property
    -> [LazyJsonAttributePropertyIgnore()]  \
    &nbsp; &nbsp; &nbsp;public Int32 Internalid { get; set; }
  - ##### Support easily property renaming by adding attribute to property
    -> [LazyJsonAttributePropertyRename("NewName")]  \
    &nbsp; &nbsp; &nbsp;public Decimal Amount { get; set; }
  - ##### Support specific Serializers\Deserializers by adding attribute to property
    -> [LazyJsonAttributeTypeSerializer(typeof(YourTypeSerializer))]  \
    -> [LazyJsonAttributeTypeDeserializer(typeof(YourTypeDeserializer))]  \
    &nbsp; &nbsp; &nbsp;public YourType YourProperty { get; set; }

- #### Usage
  - ##### Reader
    -> LazyJson lazyJson = LazyJsonReader.Read(yourJson);
  - ##### Writer
    -> String yourJson = LazyJsonWriter.Write(lazyJson);
  - ##### Serialization
    -> String yourJson = LazyJsonSerializer.Serialize(yourObject);  \
    -> LazyJsonToken jsonToken = LazyJsonSerializer.SerializeToken(yourObject);
  - ##### Deserialization
    -> YourObject yourObject = (YourObject)LazyJsonDeserializer.Deserialize(yourJson, typeof(YourObject));  \
    -> YourObject yourObject = (YourObject)LazyJsonDeserializer.DeserializeToken(jsonToken, typeof(YourObject));  \
    -> YourObject yourObject = LazyJsonDeserializer\<YourObject\>.Deserialize(yourJson);  \
    -> YourObject yourObject = LazyJsonDeserializer\<YourObject\>.DeserializeToken(jsonToken);
