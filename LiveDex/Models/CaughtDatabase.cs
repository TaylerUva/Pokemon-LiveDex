using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace LiveDex.Models {
    public class CaughtDatabase {

        readonly SQLiteAsyncConnection caughtDatabase;

        public CaughtDatabase(string dbPath) {
            caughtDatabase = new SQLiteAsyncConnection(dbPath);
            caughtDatabase.CreateTableAsync<CaughtModel>().Wait();
        }

        public async void PopulateDatabase() {
            if ((await GetAllPokemonCaught()).Count != PokeData.MAX_DEX_NUM) {
                for (int id = PokeData.MIN_DEX_NUM; id < PokeData.MAX_DEX_NUM; id++) {
                    var pokemon = new CaughtModel { ID = id, Obtained = false };
                    await caughtDatabase.InsertAsync(pokemon);
                }
            }
        }

        public Task<int> SetCaughtStatus(CaughtModel item) {
            return caughtDatabase.UpdateAsync(item);
        }

        private Task<CaughtModel> GetItemAsync(int id) {
            return caughtDatabase.Table<CaughtModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async void GetCaughtStatus(int id) {
            var pokemon = await GetItemAsync(id);
            PokeData.Caught[id] = pokemon.Obtained;
        }

        public Task<List<CaughtModel>> GetAllPokemonCaught() {
            return caughtDatabase.QueryAsync<CaughtModel>("SELECT * FROM [CaughtModel]");
        }
    }
}
