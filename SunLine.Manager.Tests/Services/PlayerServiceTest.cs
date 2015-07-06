using System.Linq;
using Xunit;
using Moq;
using Autofac;
using SunLine.Manager.Tests;
using SunLine.Manager.DataTransferObjects.Request;
using SunLine.Manager.Entities.Core;
using SunLine.Manager.Entities.Football;
using SunLine.Manager.Repositories.Infrastructure;
using SunLine.Manager.Services.Core;
using SunLine.Manager.Services.Football;

namespace SunLine.Manager.Tests
{
	public class PlayerServiceTest : DependencyInjectedBaseTest
	{
		[Fact]
		public void GeneratedPlayersInTeamMustBeProperFor442ATeamSettings()
		{
			Assert.True(false);
		}
	}
}	