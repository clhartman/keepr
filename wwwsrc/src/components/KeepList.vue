<template>
  <div class="keeplist">
    <h1>Keeps Draw here</h1>
    <div class="container" id="public-keeps">
      <div class="row">
        <div class="col" v-for="keep in keeps">
          <div class="card" style="width: 18rem;">
            <img :src="keep.img" class="card-img-top">
            <div class="card-body">
              <h5 class="card-title">{{keep.name}}</h5>
              <p class="card-text">{{keep.description}}
              </p>
              <button class="btn btn-primary">Views:{{keep.views}}</button>
              <button class="btn btn-primary">Keeps:{{keep.keeps}}</button>
              <button class="btn btn-primary">Shares:{{keep.shares}}</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "KeepList",
    props: [],
    mounted() {
      this.$store.dispatch("getPublicKeeps");
    },
    data() {
      return {}
    },
    computed: {
      keeps() {
        return this.$store.state.publicKeeps;
      }
    },
    methods: {},
    components: {}
  }
</script>