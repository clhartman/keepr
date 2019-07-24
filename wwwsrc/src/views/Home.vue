<template>
  <div class="home">
    <h1>Welcome Home {{user.username}}</h1>
    <button v-if="user.id" @click="logout">logout</button>
    <router-link v-else :to="{name: 'login'}">Login</router-link>
    <div class="col">
      <h6 class="title">Create a Keep</h6>
      <form @submit.prevent="addKeep">
        <input type="text" class="form-control" placeholder="title" v-model="newKeep.name" required>
        <input type="text" class="form-control" placeholder="description" v-model="newKeep.description">
        <input type="text" class="form-control" placeholder="image" v-model="newKeep.img">
        <input type="checkbox" v-model="newKeep.isPrivate">Private: {{newKeep.isPrivate}}
        <button class="btn btn-secondary" type="submit">Create Keep</button>
      </form>
    </div>
    <KeepList />
    <VaultList />
    <UserKeeps />
  </div>
</template>

<script>

  import KeepList from '@/components/KeepList.vue';
  import VaultList from '@/components/VaultList.vue';
  import UserKeeps from '@/components/UserKeeps.vue';
  export default {
    name: "home",
    computed: {
      user() {
        return this.$store.state.user;
      }
    },
    data() {
      return {
        newKeep: {
          name: "",
          description: "",
          img: "",
          isPrivate: ""
        }
      }
    },
    components: {
      KeepList,
      VaultList,
      UserKeeps
    },
    methods: {
      logout() {
        this.$store.dispatch("logout");
      },
      addKeep() {
        this.$store.dispatch("addKeep", this.newKeep);
        this.newKeep = { title: "", description: "", img: "", isPrivate: "" }
      }
    }
  };
</script>