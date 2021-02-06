import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost")
  ? "https://localhost:5001/"
  : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true,
});

export default new Vuex.Store({
  state: {
    publicKeeps: [],
    myKeeps: [],
    myVaults: [],
    userVKs: [],
    vaultKeeps: [],
    keepsByVault: [],
    activeKeep: {},
    activeVault: {},
  },
  mutations: {
    //#region mutation KEEPS
    setPublicKeeps(state, keeps) {
      state.publicKeeps = keeps;
    },
    setUserKeeps(state, keeps) {
      state.myKeeps = keeps;
    },
    setActiveKeep(state, keep) {
      state.activeKeep = keep;
    },
    addKeep(state, newKeep) {
      state.myKeeps.push(newKeep);
    },
    updateMyKeep(state, update) {
      let index = -1;
      index = state.myKeeps.findIndex((v) => v.id == update.id);
      state.myKeeps.splice(index, 1);
      state.myKeeps.push(update);
      index = state.publicKeeps.findIndex((v) => v.id == update.id);
      state.publicKeeps.splice(index, 1);
      state.publicKeeps.push(update);
    },
    removeKeep(state, id) {
      let index = state.myKeeps.findIndex((k) => k.id == id);
      state.myKeeps.splice(index, 1);
    },
    //#endregion mutation KEEPS

    //#region mutation VAULTS
    setMyVaults(state, vaults) {
      state.myVaults = vaults;
    },
    addVault(state, newVault) {
      state.myVaults.push(newVault);
    },
    updateVault(state, update) {
      let index = state.myVaults.findIndex((v) => v.id == update.id);
      state.myVaults.splice(index, 1);
      state.myVaults.push(update);
    },
    setActiveVault(state, vault) {
      state.activeVault = vault;
    },
    removeVault(state, id) {
      let index = state.myVaults.findIndex((v) => v.id == id);
      state.myVaults.splice(index, 1);
    },
    //#endregion mutation VAULTS

    //#region mutation VAULTKEEPS
    setUserVKs(state, userVKs) {
      state.userVKs = userVKs;
    },
    setKeepsByVault(state, keepsByVault) {
      state.keepsByVault = keepsByVault;
    },
    // all vaultkeeps
    addVK(state, newVK) {
      state.vaultKeeps.push(newVK);
    },
    removeVK(state, id) {
      let index = state.vaultKeeps.findIndex((vk) => vk.id == id);
      state.vaultKeeps.splice(index, 1);
    },
    //#endregion mutation VAULTKEEPS
  },

  actions: {
    setBearer({ dispatch }, bearer) {
      api.defaults.headers.authorization = bearer;
      dispatch("getUserVaults");
      dispatch("getUserKeeps");
      dispatch("getUserVKs");
      dispatch("getKeeps");
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },

    //#region actions KEEPS
    // getKeeps only returns isPrivate = false (Public)
    async getKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps");
        commit("setPublicKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    // getUserKeeps returns both private and public this specific user
    async getUserKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps/user");
        commit("setUserKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getKeepById({ commit, dispatch }, id) {
      try {
        let res = await api.get("keeps/" + id);
        commit("setActiveKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async createKeep({ commit, dispatch }, newKeep) {
      try {
        let res = await api.post("keeps", newKeep);
        dispatch("getUserKeeps");
        commit("addKeep", res.data);
        commit("setActiveKeep", res.data);
        router.push({ name: "keepdetails", params: { keepId: res.data.id } });
        return res.data;
      } catch (error) {
        console.error(error);
      }
    },
    async editKeep({ commit, dispatch }, update) {
      try {
        let res = await api.put("keeps/" + update.id, update);
        debugger;
        commit("updateMyKeep", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async deleteKeep({ commit, dispatch }, id) {
      try {
        let res = await api.delete("keeps/" + id);
        commit("removeKeep", id);
        router.push({ name: "dashboard", params: {} });
      } catch (error) {
        console.error(error);
      }
    },
    //#endregion actions KEEPS

    //#region actions VAULTS
    async getUserVaults({ commit, dispatch }) {
      try {
        let res = await api.get("vaults");
        commit("setMyVaults", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async createVault({ commit, dispatch }, newVault) {
      try {
        let res = await api.post("vaults", newVault);
        commit("addVault", res.data);
        commit("setActiveVault", res.data);
        return res.data;
      } catch (error) {
        console.error(error);
      }
    },
    async editVault({ commit, dispatch }, update) {
      try {
        let res = await api.put("vaults/" + update.id, update);
        commit("updateVault", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getVaultById({ commit, dispatch }, id) {
      try {
        let res = await api.get("vaults/" + id);
        commit("setActiveVault", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async deleteVault({ commit, dispatch }, id) {
      try {
        let res = await api.delete("vaults/" + id);
        commit("removeVault", id);
        router.push({ name: "dashboard", params: {} });
      } catch (error) {
        console.error(error);
      }
    },
    //#endregion actions VAULTS

    //#region actions VAULTKEEPS
    async getUserVKs({ commit, dispatch }) {
      try {
        let res = await api.get("vaultkeeps");
        commit("setUserVKs", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getKeepsByVaultId({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps");
        commit("setKeepsByVault", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async createVK({ commit, dispatch }, newVK) {
      try {
        let res = await api.post("vaultkeeps", newVK);
        dispatch("getKeepsByVaultId", newVK.vaultId);
        commit("addVK", res.data);
        router.push({
          name: "vaultdetails",
          params: { vaultId: res.data.vaultId },
        });
        return res.data;
      } catch (error) {
        console.error(error);
      }
    },
    async deleteVK({ commit, dispatch }, keepVM) {
      try {
        let res = await api.delete("vaultkeeps/" + keepVM.vaultKeepId, keepVM);
        commit("removeVK", keepVM.vaultKeepId);
        keepVM.keeps -= 1;
        dispatch("getKeepsByVaultId", keepVM.vaultId); // Required to render the removed Keep
        if (keepVM.keeps < 1) {
          router.push({
            name: "dashboard",
            params: {},
          });
        }
      } catch (error) {
        console.error(error);
      }
    },
    //#endregion actions VAULTKEEPS
  },
});
