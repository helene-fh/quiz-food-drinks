﻿using System;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace quiz_food_drinks.Configurations.SwaggerSchema
{
	public class SwaggerSchemaExampleFIlter : ISchemaFilter
	{
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (context.MemberInfo != null)
			{

				var schemaAttribute = context.MemberInfo.GetCustomAttributes<SwaggerSchemaExampleAttribute>()
					.FirstOrDefault();
				if (schemaAttribute != null)
				{
					ApplySchemaAttribute(schema, schemaAttribute);
				}
			}

		}

		private void ApplySchemaAttribute(OpenApiSchema schema, SwaggerSchemaExampleAttribute schemaAttribute)
		{

			if (schemaAttribute.Example != null)
			{
				schema.Example = new Microsoft.OpenApi.Any.OpenApiString(schemaAttribute.Example);
			}

		}

	}
}

