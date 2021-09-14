<template>
  <div id="app">
      <TheHeader/>
      <TheContent/>
  </div>
</template>

<script>
import TheHeader from "./components/layout/TheHeader.vue"
import TheContent from "./components/layout/TheContent.vue"
export default {
  name: 'App',
  components: {
    TheHeader,
    TheContent
  }
}
</script>

<style scoped>
    @import "../src/css/app.css";
</style>

