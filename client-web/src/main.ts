import { createPromiseClient } from "@connectrpc/connect";
import { createGrpcWebTransport } from "@connectrpc/connect-web";
import { DiscountService, GenerateRequest, UseCodeRequest } from "./proto/discounts";

const app = document.getElementById("app")!;
app.innerHTML = `
  <main style="font-family:system-ui,Segoe UI,Roboto,Arial;padding:2rem;max-width:720px;margin:0 auto;">
    <h1>Discounts — Cliente gRPC-Web</h1>
    <div style="display:grid;gap:12px;margin-top:12px;">
      <label>Base URL <input id="base" value="http://localhost:8080" style="width:100%;padding:8px;"></label>
      <fieldset style="padding:12px"><legend>Generate</legend>
        <label>Count <input id="count" type="number" value="10" style="width:90px;padding:6px;"></label>
        <label>Length <input id="length" type="number" value="8" style="width:90px;padding:6px;"></label>
        <button id="btnGen">Generate</button>
      </fieldset>
      <fieldset style="padding:12px"><legend>UseCode</legend>
        <label>Code <input id="code" type="text" placeholder="ABCDEFGH" style="width:200px;padding:6px;"></label>
        <button id="btnUse">Use</button>
      </fieldset>
      <pre id="out" style="background:#111;color:#0f0;padding:12px;border-radius:8px;min-height:120px;white-space:pre-wrap;"></pre>
    </div>
  </main>
`;

const out = document.getElementById("out") as HTMLPreElement;
const get = (id: string) => document.getElementById(id) as HTMLInputElement;
function log(x: unknown) {
  out.textContent = (out.textContent ? out.textContent + "\n" : "") + JSON.stringify(x, null, 2);
}

function client(base: string) {
  const transport = createGrpcWebTransport({
    baseUrl: base,
    useBinaryFormat: true,
  });
  return createPromiseClient(DiscountService, transport);
}

get("btnGen").addEventListener("click", async () => {
  const base = get("base").value.trim();
  const cnt = parseInt(get("count").value, 10);
  const len = parseInt(get("length").value, 10);
  const c = client(base);
  try {
    const res = await c.generate(new GenerateRequest({ count: cnt, length: len }));
    log({ op: "Generate", result: res });
  } catch (e) {
    log({ error: String(e) });
  }
});

get("btnUse").addEventListener("click", async () => {
  const base = get("base").value.trim();
  const code = get("code").value.trim().toUpperCase();
  const c = client(base);
  try {
    const res = await c.useCode(new UseCodeRequest({ code }));
    log({ op: "UseCode", result: res });
  } catch (e) {
    log({ error: String(e) });
  }
});