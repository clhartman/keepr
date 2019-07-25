<template>
  <div class="vaultlist">
    <h1>My Vaults</h1>
    <div class="container" id="user-vaults">
      <div class="row">
        <div class="col">
          <h6 class="title">Create a Vault</h6>
          <form @submit.prevent="addVault">
            <button type="button" class="close waves-effect waves-light" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">x</span>
            </button>
            <input type="text" class="form-control" placeholder="title" v-model="newVault.name" required>
            <input type="text" class="form-control" placeholder="description" v-model="newVault.description">
            <button class="btn btn-secondary" type="submit">Create Vault</button>
          </form>
        </div>
        <div class="col" v-for="vault in vaults" :key="vault.id">
          <div class="card bg-dark text-white">
            <img src="https://sfj70.typepad.com/.a/6a00d834515ddf69e20154369e6029970c-800wi" class="card-img" alt="...">
            <div class="card-img-overlay">
              <h5 class="card-title">{{vault.name}}</h5>
              <p class="card-text">{{vault.description}}</p>
              <router-link class="btn btn-secondary" :to="{ name: 'vault', params: {vaultId: vault.id}}">Enter Vault
              </router-link>
              <button class="btn btn-danger" @click="deleteVault(vault)">Delete Vault</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "VaultList",
    props: [],
    mounted() {
      this.$store.dispatch("getUserVaults");
    },
    data() {
      return {
        newVault: {
          name: "",
          description: ""
        }
      }
    },
    computed: {
      user() {
        return this.$store.state.user;
      },
      vaults() {
        return this.$store.state.userVaults;
      }
    },
    methods: {
      addVault() {
        this.$store.dispatch("addVault", this.newVault);
        this.newVault = { title: "", description: "" };
      },
      deleteVault(vault) {
        console.log(vault)
        this.$store.dispatch("deleteVault", vault);
      }

    },
    components: {}
  }
</script>