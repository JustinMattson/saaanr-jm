<template>
  <div class="profile container p-0">
    <!-- KEEP TEMPLATE -->
    
    This is the profile component.
    <!-- END KEEP TEMPLATE -->
  </div>
</template>


<script>
export default {
  name: 'profile',
  data(){
    return {}
  },
  computed: {
    activeProfile() {
      return this.$store.state.activeProfile;
    },
    profiles() {
      return this.$store.state.profiles;
    },
  },
  methods:{},
  components:{}
}
</script>


<style scoped>

</style>