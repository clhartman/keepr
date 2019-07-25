<template>
  <div class="Keep">
    <div class="card" style="width: 18rem;">
      <img :src="keepObj.img" class="card-img-top">
      <div class="card-body">
        <h5 class="card-title">{{keepObj.name}}</h5>
        <p class="card-text">{{keepObj.description}}</p>
        <p class="card-text">Keeps: {{keepObj.keeps}}</p>
        <button class="btn btn-primary" @click="viewKeep">Views:{{keepObj.views}}</button>
        <select v-if="!onLogin" v-model="selected" @change="onChange($event.target.value)">
          <!-- <option disabled value>Keeps: {{keepObj.keeps}}</option> -->
          <option v-for="vault in vaults" :value="vault.id">{{vault.name}}</option>
        </select>
        <button class="btn btn-primary" @click="shareKeep">Shares:{{keepObj.shares}}</button>
        <button class="btn btn-danger" @click="removeKeep($route.params.vaultId)" v-if="$route.params.vaultId">Remove
          from
          Vault</button>
      </div>
    </div>
    <div v-if="showModal">
      <transition name="modal">
        <div class="modal-mask">
          <div class="modal-wrapper">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title">{{keepObj.name}}</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true" @click="showModal = false">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <img :src="keepObj.img" class="card-img-top">
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" @click="showModal = false">Close</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
  export default {
    name: "Keep",
    props: { keepObj: Object },
    data() {
      return {
        selected: "",
        showModal: false,
        onLogin: this.$route.fullPath === '/login'
      }
    },
    mounted() {
      if (!this.onLogin) {
        this.$store.dispatch("getUserVaults")
      }
    },
    computed: {
      vaults() {
        return this.$store.state.userVaults;
      }
    },
    methods: {
      onChange(vaultId) {
        const keepId = this.keepObj.id
        this.keepObj.keeps++
        this.$store.dispatch("updateKeep", this.keepObj)
        this.$store.dispatch("saveKeepToVault", { vaultId, keepId })
      },
      viewKeep() {
        this.showModal = true
        this.keepObj.views++
        this.$store.dispatch("updateKeep", this.keepObj)
      },
      shareKeep() {
        this.keepObj.shares++
        this.$store.dispatch("updateKeep", this.keepObj)
      },
      removeKeep(vaultId) {
        const keepId = this.keepObj.id
        this.keepObj.keeps--
        this.$store.dispatch("updateKeep", this.keepObj)
        this.$store.dispatch("removeKeepFromVault", { vaultId, keepId })
      }
    },
    components: {}
  }
</script>
<style>
  .modal-mask {
    position: fixed;
    z-index: 9998;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, .5);
    display: table;
    transition: opacity .3s ease;
  }

  .modal-wrapper {
    display: table-cell;
    vertical-align: middle;
  }
</style>