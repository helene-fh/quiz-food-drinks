using System;
namespace quiz_food_drinks.Configurations.SwaggerSchema
{

	[AttributeUsage(
		AttributeTargets.Class |
		AttributeTargets.Struct |
		AttributeTargets.Parameter |
		AttributeTargets.Property |
		AttributeTargets.Enum,
		AllowMultiple = false)]


	public class SwaggerSchemaExampleAttribute : Attribute
	{
		public string Example;

		public SwaggerSchemaExampleAttribute(string example)
		{
			Example = example;
		}
	}
}

