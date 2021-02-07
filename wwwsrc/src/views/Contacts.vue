<template>
  <div class="contacts container-fluid p-2">
    <div class="row">
      <div class="col text-center text-shadow">
        <h1>Northern Rockies - Contacts!</h1>
      </div>
      <!-- add modal to add contact if logged in -->
      <div class="col-12 list-container d-flex justify-content-center">
        <div id="contacts" class="card-columns p-2" style="column-gap: 1rem;">
          <!-- CONTACT COMPONENTS BEGIN -->
          <contact v-for="contact in contacts" :key="contact.id" :contact="contact" />
          <!-- CONTACT COMPONENTS END -->
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
import Contact from "@/components/ContactComponent.vue";
export default {
  name: "contacts",
  data() {
    return {
      newContact: {}
    };
  },
  async mounted() {
    await this.$store.dispatch("getContacts");
  },
  computed: {
    contacts() {
      return this.$store.state.contacts;
    }
  },
  methods: {},
  components: {
    Contact
  }
};
</script>

<style scoped></style>
