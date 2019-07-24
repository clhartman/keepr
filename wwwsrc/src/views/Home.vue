<template>
  <div class="home">
    <h1>Welcome Home {{user.username}}</h1>
    <button v-if="user.id" @click="logout">logout</button>
    <router-link v-else :to="{name: 'login'}">Login</router-link>
    <KeepList />
    <VaultList />
  </div>
</template>

<script>

  import KeepList from '@/components/KeepList.vue';
  import VaultList from '@/components/VaultList.vue'
  export default {
    name: "home",
    computed: {
      user() {
        return this.$store.state.user;
      }
    },
    components: {
      KeepList,
      VaultList
    },
    methods: {
      logout() {
        this.$store.dispatch("logout");
      }
    }
  };
</script>