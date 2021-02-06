import Vue from "vue";
import Router from "vue-router";
import About from "./views/About.vue";
import Dashboard from "./views/Dashboard.vue";
import Database from "./views/Database.vue";
import Home from "./views/Home.vue";
import KeepDetails from "./views/KeepDetails.vue";
import Pics from "./views/Pics.vue";
import Judges from "./views/Judges.vue";
import Practice from "./views/Practice.vue";
import Profile from "./views/Profile.vue";
import Schedule from "./views/Schedule.vue";
import VaultDetails from "./views/VaultDetails.vue";
import { authGuard } from "@bcwdev/auth0-vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home,
    },
    {
      path: "/about",
      name: "about",
      component: About,
    },
    {
      path: "/dashboard",
      name: "dashboard",
      component: Dashboard,
      beforeEnter: authGuard,
    },
    {
      path: "/dashboard/:vaultId",
      name: "vaultdetails",
      component: VaultDetails,
      beforeEnter: authGuard,
    },
    {
      path: "/database",
      name: "database",
      component: Database,
      beforeEnter: authGuard,
    },
    {
      path: "/keeps/:keepId",
      name: "keepdetails",
      component: KeepDetails,
    },
    {
      path: "/pics",
      name: "pics",
      component: Pics,
    },
    {
      path: "/judges",
      name: "judges",
      component: Judges,
    },
    {
      path: "/practice",
      name: "practice",
      component: Practice,
    },
    {
      path: "/profile",
      name: "profile",
      component: Profile,
      beforeEnter: authGuard,
    },
    {
      path: "/schedule",
      name: "schedule",
      component: Schedule,
    },
  ],
});
