<template>
  <div class="Vault">
    <h3>Keeps in Vault: {{vaultkeeps.length}}</h3>
    <KeepList v-bind:keeps="vaultkeeps" />
  </div>
</template>

<script>
  import KeepList from "@/components/KeepList.vue"
  export default {
    name: "Vault",
    props: ["id"],
    mounted() {
      this.$store.dispatch('getVaultKeeps', this.$route.params.vaultId)
    },
    data() {
      return {}
    },
    computed: {
      vaultkeeps() {
        return this.$store.state.vaultkeeps
      }
    },
    methods: {},
    components: {
      KeepList
    }
  }
</script>