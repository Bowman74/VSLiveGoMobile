using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GoMobile.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(GoMobile.Services.MockDataStore))]
namespace GoMobile.Services
{
	public class MockDataStore : IDataStore<Recording>
	{
		bool isInitialized;
		List<Recording> recordings;

		public async Task<bool> AddItemAsync(Recording item)
		{
			await InitializeAsync();

            recordings.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateItemAsync(Recording item)
		{
			await InitializeAsync();

			var _item = recordings.FirstOrDefault((Recording arg) => arg.Id == item.Id);
            if (_item != null)
            {
                recordings.Remove(_item);
                recordings.Add(item);
            }

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteItemAsync(Recording item)
		{
			await InitializeAsync();

			var _item = recordings.FirstOrDefault((Recording arg) => arg.Id == item.Id);
            if (_item != null)
            {
                recordings.Remove(_item);
            }

			return await Task.FromResult(true);
		}

		public async Task<Recording> GetItemAsync(string id)
		{
			await InitializeAsync();

			return await Task.FromResult(recordings.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<Recording>> GetItemsAsync(bool forceRefresh = false)
		{
			await InitializeAsync();

			return await Task.FromResult(recordings);
		}

		public Task<bool> PullLatestAsync()
		{
			return Task.FromResult(true);
		}


		public Task<bool> SyncAsync()
		{
			return Task.FromResult(true);
		}

		public async Task InitializeAsync()
		{
			if (isInitialized)
				return;

			recordings = new List<Recording>();
			var _recordings = new List<Recording>
			{
				new Recording { Id = Guid.NewGuid().ToString(), Description="The cats are hungry"},
				new Recording { Id = Guid.NewGuid().ToString(), Description="Seems like a functional idea"},
				new Recording { Id = Guid.NewGuid().ToString(), Description="Noted"},
				new Recording { Id = Guid.NewGuid().ToString(), Description="Pine and cranberry for that winter feel"},
				new Recording { Id = Guid.NewGuid().ToString(), Description="Keep it a secret!"},
				new Recording { Id = Guid.NewGuid().ToString(), Description="Done"},
			};

			foreach (Recording item in _recordings)
			{
                recordings.Add(item);
			}

			isInitialized = true;
		}
	}
}
