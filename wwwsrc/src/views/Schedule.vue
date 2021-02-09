<template>
  <div class="schedule">
    <div class="row p-3">
      <div class="col-12">

      <h1>Upcoming Events:</h1>
      </div>
      <div class="col-6">
      <event v-for="event in events" :key="event.id" :event="event" />
      </div>

    </div>
  </div>
</template>


<script>
import Event from "@/components/EventComponent.vue";
import Contact from "@/components/ContactComponent.vue";
export default {
  name: 'schedule',
  data(){
    return {}
  },
  async mounted() {
    await this.$store.dispatch("getEvents");
  },
  computed:{
    events() {
      return this.$store.state.events;
    },
    contacts() {
      return this.$store.state.contacts;
    },
  },
  methods:{},
  components:{
    Event,
    Contact
  }
}
</script>


<style scoped>

</style>