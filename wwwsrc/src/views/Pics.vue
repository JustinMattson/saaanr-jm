<template>
  <div class="pics container-fluid p-2">
    <div class="row">
      <div class="col text-center text-shadow">
        <h1>Welcome to Northern Rockies SAAA!</h1>
      </div>
      <!-- add modal to add keep if logged in -->
      <div class="col-12 list-container d-flex justify-content-center">
        <div id="keeps" class="card-columns p-2" style="column-gap: 1rem;">
          <!-- KEEP COMPONENTS BEGIN -->
          <keep v-for="keep in publicKeeps" :key="keep.id" :keep="keep" />
          <!-- KEEP COMPONENTS END -->
        </div>
      </div>
    </div>
    <small class="fixed-bottom">
      Made with
      <i class="far fa-heart text-danger" title="Boise Codeworks" /> by Justin
      Mattson!  <a href="https://www.nvaccess.org/"> - a11y - </a>
    </small>
  </div>
</template>

<script>
import Keep from "@/components/KeepComponent.vue";
export default {
  name: "pics",
  data() {
    return {
      newKeep: {}
    };
  },
  async mounted() {
    await this.$store.dispatch("getKeeps");
  },
  computed: {
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    myVaults() {
      return this.$store.state.myVaults;
    }
  },
  methods: {
    // logout() {
    //   this.$store.dispatch("logout");
    // }
  },
  components: {
    Keep
  }
};
</script>

<style scoped></style>
