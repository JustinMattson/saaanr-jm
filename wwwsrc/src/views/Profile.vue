<template>
  <div class="profile pics container-fluid p-2">
    <div class="row">
      <div class="col text-center text-shadow">
      <h1 class="p-5">This is where you will be able to change your Profile name, Profile image, see your membership status, potentially your PRs, game registration status etc.</h1>
      </div>
      <!-- add modal to add profile if logged in -->
      <div class="col-12 list-container d-flex justify-content-center">
        <div id="profiles" class="card-columns p-2" style="column-gap: 1rem;">
          <!-- PROFILE COMPONENTS BEGIN -->
          <profile v-for="profile in profiles" :key="profile.id" :profile="profile" />
          <!-- PROFILE COMPONENTS END -->
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
import Profile from "@/components/ProfileComponent.vue";
export default {
  name: 'profile',
  data(){
    return {
      newProfile: {}
    };
  },
  async mounted() {
    await this.$store.dispatch("getProfile");
  },
  computed:{
    profile() {
      return this.$store.state.activeProfile;
    },
    profiles() {
      return this.$store.state.profiles;
    }
  },
  methods:{},
  components:{
    Profile
  }
}
</script>


<style scoped>

</style>