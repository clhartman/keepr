<template>
  <div class="Dashboard">
    <router-link class="btn btn-secondary" :to="{name: 'home'}">Go Home</router-link>
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
    <UserKeeps />
    <VaultList />
  </div>
</template>

<script>
  import UserKeeps from '@/components/UserKeeps.vue';
  import VaultList from '@/components/VaultList.vue';
  export default {
    name: "Dashboard",
    props: [],
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
    mounted() {
      this.$store.dispatch("getUserKeeps")
    },
    computed: {
      keeps() {
        return this.$store.state.userKeeps;
      },
    },
    methods: {
      addKeep() {
        this.$store.dispatch("addKeep", this.newKeep);
        this.newKeep = { title: "", description: "", img: "", isPrivate: "" }
      }
    },
    components: {
      VaultList,
      UserKeeps
    }
  }
</script>