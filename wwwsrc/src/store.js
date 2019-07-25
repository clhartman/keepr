import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'
import AuthService from './AuthService'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? '//localhost:5000/' : '/'

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    publicKeeps: [],
    userKeeps: [],
    userVaults: [],
    vaultkeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    resetState(state) {
      //clear the entire state object of user data
      state.user = {}
      state.userKeeps = []
      state.userVaults = []
    },
    setPublicKeeps(state, keeps) {
      state.publicKeeps = keeps
    },
    setUserKeeps(state, keeps) {
      state.userKeeps = keeps
    },
    setUserVaults(state, vaults) {
      state.userVaults = vaults
    },
    setVaultKeeps(state, vaultkeeps) {
      state.vaultkeeps = vaultkeeps
    }
  },
  actions: {
    // #region AUTH 
    async register({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Register(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async login({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Login(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async logout({ commit, dispatch }) {
      try {
        let success = await AuthService.Logout()
        if (!success) { }
        commit('resetState')
        router.push({ name: "login" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    // #endregion
    getPublicKeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => {
          commit('setPublicKeeps', res.data)
        })
    },
    getUserKeeps({ commit, dispatch }) {
      api.get('keeps/user')
        .then(res => {
          commit("setUserKeeps", res.data)
        })
    },
    getVaultKeeps({ commit, dispatch }, vaultId) {
      const url = 'vaultkeeps/' + vaultId;
      console.log(url)
      api.get(url)
        .then(res => {
          console.log(res)
          commit("setVaultKeeps", res.data)
        })
    },
    addKeep({ commit, dispatch }, keepData) {
      api.post('keeps', keepData)
        .then(res => {
          dispatch("getPublicKeeps")
        })
    },

    // saveKeepToVault({ commit, dispatch }, { vaultId, keepId }) {
    saveKeepToVault({ commit, dispatch }, payload) {
      //creating variables that are getting assigned values by pulling the values from payload with the same name as the key
      // const { vaultId, keepId } = payload;
      // console.log(`vaultId: ${vaultId}, keepId: ${keepId}`)
      api.post('vaultkeeps', payload)
        .then(res => {
        })
    },
    removeKeepFromVault({ commit, dispatch }, payload) {
      api.put('vaultkeeps', payload)
        .then(res => {
          dispatch('getVaultKeeps', payload.vaultId)
        })
    },
    updateKeep({ commit, dispatch }, payload) {
      api.put('keeps/' + payload.id, payload)
        .then(res => {
        })
    },
    deleteKeep({ commit, dispatch }, keep) {
      api.delete('keeps/' + keep.id)
        .then(res => {
          dispatch('getUserKeeps')
          dispatch('getPublicKeeps')
        })
    },
    getUserVaults({ commit, dispatch }) {
      api.get('vaults')
        .then(res => {
          commit("setUserVaults", res.data)
        })
    },
    addVault({ commit, dispatch }, vaultData) {
      api.post('vaults', vaultData)
        .then(res => {
          dispatch("getUserVaults")
        })
    },
    deleteVault({ commit, dispatch }, vault) {
      console.log(vault.id)
      api.delete('vaults/' + vault.id)
        .then(res => {
          dispatch('getUserVaults')
        })
    }
  }

})
