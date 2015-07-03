using System;
using System.ComponentModel.DataAnnotations;
using SunLine.Manager.Entities;

namespace SunLine.Manager.WebApi.DataTransferObject
{
	public class BaseEntityDto
	{	
		public BaseEntityDto()
		{
		}
		
		public BaseEntityDto(BaseEntity baseEntity)
		{
			Id = baseEntity.Id;
			Version = baseEntity.Version;
			CreationDate = baseEntity.CreationDate;
			ModificationDate = baseEntity.ModificationDate;
		}
		
        public int Id { get; set; }

        public int Version { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
	}
}