using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace LiveDex.Models {
    public class CaughtDatabase {

        public static bool populated;

        readonly SQLiteAsyncConnection caughtDatabase;

        public CaughtDatabase(string dbPath) {
            caughtDatabase = new SQLiteAsyncConnection(dbPath);
            caughtDatabase.CreateTableAsync<CaughtModel>().Wait();
        }


        public async Task PopulateDatabase(bool populateWith) {
            await PopulateDatabase(populateWith, false);
        }

        // TODO: Fix it to allow more pokemon without breaking current database
        public async Task PopulateDatabase(bool populateWith, bool clearFirst) {
            if (clearFirst) {
                await caughtDatabase.DeleteAllAsync<CaughtModel>();
            } else if (populated) return;
            if ((await GetAllPokemonCaught()).Count != PokeData.MAX_DEX_NUM) {
                for (int id = 0; id < PokeData.MAX_DEX_NUM; id++) {
                    PokeData.Caught[id].Obtained = populateWith;
                    var pokemon = new CaughtModel { ID = id, Obtained = populateWith };
                    await caughtDatabase.InsertAsync(pokemon);
                }
            }
            populated = true;
        }

        public Task<int> SetCaughtStatus(CaughtModel item) {
            return caughtDatabase.UpdateAsync(item);
        }

        private Task<CaughtModel> GetItemAsync(int id) {
            return caughtDatabase.Table<CaughtModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async void GetCaughtStatus(int id) {
            var pokemon = await GetItemAsync(id);
            PokeData.Caught[id].Obtained = pokemon.Obtained;
        }

        public Task<List<CaughtModel>> GetAllPokemonCaught() {
            return caughtDatabase.QueryAsync<CaughtModel>("SELECT * FROM [CaughtModel]");
        }
    }
}
