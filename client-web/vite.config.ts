// client-web/vite.config.ts
import { defineConfig } from "vite";

export default defineConfig({
  base: "/web/",
  build: {
    outDir: "dist",
    assetsDir: "assets"
  }
});
