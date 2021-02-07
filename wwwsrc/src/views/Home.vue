<template>
  <div class="home container-fluid">
    <div class="row m-0">
      <div class="col-12 text-center text-shadow">
        <h1 class="p-3 m-0">Welcome to the new Northern Rockies SAAA home page!</h1>
      </div>
      <div class="col-sm-6 order-sm-1 px-3">
        <img class="border border-light" src="../assets/SAAANR.png" style="max-width:100%;max-height:100%;" alt="SAAA Northern Rockies crest badge viking longboat" />
      </div>
      <div class="col-sm-6 order-sm-2 text-left m-0 align-self-center">
        <h2 class="p-3 m-0">Non-Profit </h2>
        <h2 class="p-3 m-0">Contact Information</h2>
        <h2 class="p-3 m-0">Paypal/Donation</h2>
      </div>
    </div>
    <small class="fixed-bottom">
      Made with
      <i class="far fa-heart text-danger" title="Lotsalove" /> by Justin
      Mattson!  <a href="https://www.nvaccess.org/"> - a11y - </a>
    </small>
  </div>
</template>

<script>
import Keep from "@/components/KeepComponent.vue";
export default {
  name: "home",
  data() {
    return {
      newKeep: {}
    };
  },
  async mounted() {
    // await this.$store.dispatch("getKeeps");
  },
  computed: {
    // user() {
    //   return this.$store.state.user;
    // },
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    myVaults() {
      return this.$store.state.myVaults;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  components: {
    Keep
  }
};
</script>

<style scoped></style>
