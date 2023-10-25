
# Lazy.Vinke.Json
.Net Json Serializer & Deserializer
&nbsp;
- ### Built-in Serializers\Deserializers
  - Type (Based on .Net Assemblies and Namespaces)
  - DataSet (Typed data table collection)
  - DataTable (Typed columns preserving row state and values)
  - DateTime (Customizable format [default: yyyy-MM-ddTHH:mm:ss:fffZ])
  - Object (Wrap\Unwrap properties declared as "Object" by exporting its instance type)
  - Generic Stack\<T\> (With write\read reverse options)
  - Generic Tuple\<T\>
  - Generic ValueTuple\<T\>
  - Generic Dictionary\<T\>
  - Generic Queue\<T\>
  - Generic List\<T\>
    &nbsp; \
    &nbsp;
- ### Supported features
  - ### Globally define Serializer\Deserializer for types just once
    LazyJsonSerializerOptions options = new LazyJsonSerializerOptions();
    options.Item\<LazyJsonSerializerOptionsGlobal\>().Add\<YourSerializer\>(typeof(SomeType));
    
    LazyJsonDeserializerOptions options = new LazyJsonDeserializerOptions();
    options.Item\<LazyJsonDeserializerOptionsGlobal\>().Add\<YourDeserializer\>(typeof(SomeType));
    &nbsp;
  - ### Support easily property ignore by adding attribute to property
    [LazyJsonAttributePropertyIgnore()]
    public Int32 Id { get; set; }
    &nbsp;
  - ### Support easily property renaming by adding attribute to property
    [LazyJsonAttributePropertyRename("NewName")]
    public Decimal Amount { get; set; }
    &nbsp;
  - ### Support specific Serializers\Deserializers by adding attribute to property
    [LazyJsonAttributeTypeSerializer(typeof(YourTypeSerializer))]
    [LazyJsonAttributeTypeDeserializer(typeof(YourTypeDeserializer))]
    public YourType YourProperty { get; set; }
    &nbsp;
- ### Usage
  - ### Reader
    LazyJson lazyJson = LazyJsonReader.Read(yourJson);
    &nbsp; \
    &nbsp;
  - ### Writer
    String yourJson = LazyJsonWriter.Write(lazyJson);
    &nbsp; \
    &nbsp;
  - ### Serialization
    String yourJson = LazyJsonSerializer.Serialize(yourObject); \
    LazyJsonToken jsonToken = LazyJsonSerializer.SerializeToken(yourObject);
    &nbsp; \
    &nbsp;
  - ### Deserialization
    YourObject yourObject = LazyJsonDeserializer\<YourObject\>.Deserialize(yourJson); \
    YourObject yourObject = LazyJsonDeserializer\<YourObject\>.DeserializeToken(jsonToken);
    &nbsp; \
    &nbsp;
