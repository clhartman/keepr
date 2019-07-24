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
              <!-- <button class="btn btn-primary" @click="addView">Views:{{keep.views}}</button> -->
              <select v-model="selected">
                <option disabled value>Keeps: {{keep.keeps}}</option>
                <option v-for="vault in vaults" :value="vault.id">{{vault.name}}</option>
              </select>
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
    props: [""],
    mounted() {
      this.$store.dispatch("getPublicKeeps");
      this.$store.dispatch("getUserVaults")
    },
    data() {
      return {
        selected: ""
      }
    },
    computed: {
      keeps() {
        return this.$store.state.publicKeeps;
      },
      vaults() {
        return this.$store.state.userVaults;
      }
    },
    methods: {
      // addView() {
      // keep.keeps++
      // this.$store.dispatch("addView", )  
      // }
    },
    components: {}
  }
</script>