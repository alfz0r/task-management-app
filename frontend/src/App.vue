<template>
  <div id="app" class="flex flex-col min-h-screen bg-gray-100">
    <Navbar />

    <main class="flex-grow container mx-auto py-8">
      <router-view />
    </main>

    <Footer />
  </div>
</template>

<script>
import Navbar from "../src/components/Navbar.vue";
import Footer from "../src/components/Footer.vue";

export default {
  name: "App",
  components: {
    Navbar,
    Footer,
  },
};
</script>