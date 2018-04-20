# PremierCrop.ADAPT.Rest
An example library for working with [ADAPT](https://github.com/ADAPT/ADAPT/) objects in a REST context.  
**This library is just a sample, not an official ADAPT project.**

## ADAPT Background
The Id properties on ADAPT objects are internal reference Ids.  For example, the Field object has integer 
FarmId and GrowerId properties:  

~~~
public class Field
{
	public CompoundIdentifier Id { get; private set; }
	public int? GrowerId { get; set; }
	public int? FarmId { get; set; }
	...
}
~~~

The CompoundIdentifier object is the main Id for an ADAPT object.  It allows
you to have multiple UniqueIds from different systems together, with a ReferenceId property:

~~~
public class CompoundIdentifier
{
	public int ReferenceId { get; set; }
	public List<UniqueId> UniqueIds { get; set; } 
}
~~~

The GrowerId and FarmId properties on the Field object are *NOT* the actual identifier used by an FMIS to identify the Farm or Grower.  Instead 
they are the ReferenceId from the Farm or Grower's CompoundIdentifier.  

ADAPT has an internal Catalog object that has lists of all objects used by an application:

~~~
public class Catalog
{
	public List<Field> Fields { get; set; }
	public List<Farm> Farms { get; set; }
	public List<Grower> Growers { get; set; }
	...
}
~~~

When you want to access the parent Farm of a Field, you can use the Field's FarmId value to look up the Farm in the Catalog's Farms List whose Id has that ReferenceId value.  

The Catalog-based approach assumes that all data referenced is in memory at one time, which is not the case when working within a web context.

## Project Goal
To make the ADAPT object and ReferenceIds useful in a web/rest context, this sample uses wrapper classes combining both the ADAPT CompoundIdentifier and [HATEOAS](https://spring.io/understanding/HATEOAS) architecture.  
 
Web API and/or API client developers would reference both the ADAPT Framework nuget package for the classes and this sample library.  

Web API developers would convert their proprietary objects to ADAPT objects and wrap them in 
ModelEnvelope instances with the associated ReferenceLinks.

API client developers would use the ModelEnvelope to get the wrapped ADAPT object and the ReferenceLinks to related objects.


# Major Classes

## ReferenceLink
Represents an HATEOAS link with the ADAPT CompoundIdentifier, when appropriate:  
~~~
public class ReferenceLink
{
	public CompoundIdentifier Id { get; set; }
	public string Rel { get; set; }
	public string Link { get; set; }
	public string Type { get; set; } = "get";
}
~~~
ReferenceLinks for child lists (Farms for a Grower) would have a null CompoundIdentifier.

## ModelEnvelope
Wraps an ADAPT object and provides ReferenceLinks to its related objects:
~~~
public class ModelEnvelope<T>
{
	public T Object { get; set; }
	public List<ReferenceLink> Links { get; set; } = new List<ReferenceLink>();
}
~~~

## ReferenceLinkClient
Wraps an HttpClient object with helper methods for calling an API using a ModelEnvelope's ReferenceLinks.  Allows 
you to make calls as follows to get the child Farms for a Grower:
~~~
var farms = await _client.GetListByRel<Farm>(grower.Links);
~~~


# Repository Projects
The following projects are included in this repository:

Project | Description
------------ | -------------
PremierCrop.ADAPT.Rest | The library itself  
PremierCrop.ADAPT.Rest.UnitTests | Unit Tests for the PremierCrop.ADAPT.Rest library
SampleObject | Library of Sample Data Transfer Objects (DTOs) an FMIS may use in their system, and example converters to transform them to ModelEnvelope objects with ReferenceLinks.
SampleObject.UnitTests | Unit Tests for the SampleObject library to show the usage of the converters
Sample.WebApi | A Web Api project utilizing the SampleObject library
Sample.ConsoleClient | A console application that uses the ReferenceLinkClient to call the Sample.WebApi

