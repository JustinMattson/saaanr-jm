<template>
  <div class="contact container-fluid">
    <div class="card border border-color=light p-3">
      <div>Contact ID: {{ contact.id }} </div>
      <div><span>Name: {{ contact.firstName }} {{ contact.lastName}}</span></div>
      <div>DOB: {{ contact.dob }} </div>
      <div>email: {{contact.email}} </div>
      <div> Verified: {{contact.verified}} </div>
      <div> Member: {{contact.member}} </div>
      <div> Picture: {{contact.picture}} </div>
      <div> Position: {{contact.position}} </div>
      <div> Show: {{contact.show}} </div>
      <div> UserId: {{contact.userId}} </div>
    </div>
  </div>
</template>


<script>
import swal from "sweetalert";
export default {
  name: 'contact',
  props: ["contact"],
  data() {
    return {}
  },
  computed:{},
  methods:{},
  components:{}
}
</script>


<style scoped>

</style>