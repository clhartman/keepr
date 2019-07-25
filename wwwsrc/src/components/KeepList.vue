<template>
  <div class="keeplist">
    <h1>Keeps Draw here</h1>
    <div class="container" id="public-keeps">
      <div class="row">
        <div class="col" v-for="keep in keeps">
          <keep v-bind:keepObj="keep" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import Keep from '@/components/Keep.vue';

  export default {
    name: "KeepList",
    props: { keeps: Array },
    mounted() {
      // this.$store.dispatch("getPublicKeeps");
    },
    computed: {
      // keeps() {
      //   return this.$store.state.publicKeeps;
      // },
    },
    methods: {
    },
    components: {
      Keep
    }
  }
</script>