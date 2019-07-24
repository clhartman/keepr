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
    userVaults: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    resetState(state) {
      //clear the entire state object of user data
      state.user = {}
    },
    setPublicKeeps(state, keeps) {
      state.publicKeeps = keeps
    },
    setUserVaults(state, vaults) {
      state.userVaults = vaults
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

    getUserVaults({ commit, dispatch }) {
      api.get('vaults')
        .then(res => {
          console.log(res.data)
          commit("setUserVaults", res.data)
        })
    },
    addVault({ commit, dispatch }, vaultData) {
      api.post('vaults', vaultData)
        .then(res => {
          dispatch("getUserVaults")
        })
    }
  }

})
