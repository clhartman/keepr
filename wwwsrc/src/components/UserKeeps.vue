<template>
  <div class="userkeeps">
    <h1>Keeps I've Made</h1>
    <div class="container" id="user-keeps">
      <div class="row">
        <div class="col" v-for="keep in keeps">
          <div class="card" style="width: 18rem;">
            <img :src="keep.img" class="card-img-top">
            <div class="card-body">
              <h5 class="card-title">{{keep.name}}</h5>
              <p class="card-text">{{keep.description}}
              </p>
              <button class="btn btn-primary" @click="addView">Views:{{keep.views}}</button>
              <button class="btn btn-primary">Shares:{{keep.shares}}</button>
              <button class="btn btn-primary">Keeps:{{keep.keeps}}</button>
              <button class="btn btn-primary">Delete My Keep</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "UserKeeps",
    props: [],
    mounted() {
      this.$store.dispatch("getUserKeeps")
    },
    data() {
      return {}
    },
    computed: {
      keeps() {
        return this.$store.state.userKeeps;
      },
      user() {
        return this.$store.state.user;
      }
    },
    methods: {
    },
    components: {}
  }
</script>