<template>
  <div class="event">
    <div class="card p-3 border border-info shadow-lg rounded-3">
      <H3>{{event.eventName}}</H3>
      <H4 class="pl-3"><small class="text-info">Date:</small> {{event.startDate}} - {{event.endDate}}</H4>
      <H4 class="pl-3"><small class="text-info">Location:</small> {{event.venue}} </H4>
      <H4 class="pl-3"><small class="text-info">Web Site:</small> {{event.website}}</H4>
      <div class="row">
        <div class="col-6"><H4><small class="text-info">Contact:</small> </H4>
          <H4 class="pl-3"><small>{{event.contactName}}</small></H4>
          <H4 class="pl-3">mailto:{{event.contactEmail}} </H4>
        </div>
        <div class="col-6"><H4><small class="text-info">Classes:</small></H4>
          <H4 class="pl-3">{{event.classes}} </H4>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'event',
  props: ['event'],
  data(){
    return {
      editForm: false,
    }
  },
  computed:{},
  methods:{},
  components:{}
}
</script>


<style scoped>

</style>