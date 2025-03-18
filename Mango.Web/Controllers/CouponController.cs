using Mango.Web.Contracts;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Mango.Web.Controllers
{
    [Route("coupon")]
    public class CouponController(ICouponService _couponService) : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var response = await _couponService.GetAllCouponsAsync();

            if (!response.IsSucceed)
            {
                return NotFound();
            }

            return View(response.Value);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _couponService.GetCouponByIdAsync(id);

            if (!result.IsSucceed)
                return NotFound();

            return View(result.Value);
        }
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CouponRequest request
            )
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = await _couponService.CreateCouponAsync(request);
            if (!response.IsSucceed) 
                return View(request);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, CancellationToken ct = default)
        {
            var coupon = await _couponService.GetCouponByIdAsync(id, ct);
            
            if (coupon == null)
            {
                return NotFound();
            }
            
            var response = new CouponRequest(coupon.Value.DiscountAmount, coupon.Value.MinAmount, coupon.Value.Code);

            return View(response);
        }

        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CouponRequest request, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            
            var result = await _couponService.UpdateCouponAsync(id, request, ct);

            if (!result.IsSucceed)
                return View(request);
            
            return RedirectToAction(nameof(Index));
        }
        [HttpGet("delete/{couponId:int}")]
        public async Task<IActionResult> Delete(int couponId)
        {
            var couponResponse = await _couponService.GetCouponByIdAsync(couponId);
            if (!couponResponse.IsSucceed)
            {
                return NotFound();
            }
            return View(couponResponse.Value);
        }

        // POST: coupon/delete/{couponId}
        [HttpPost("delete/{couponId:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int couponId, CancellationToken ct = default)
        {
            var response = await _couponService.DeleteCouponAsync(couponId, ct);
            if (!response.IsSucceed)
            {
                // Optionally: Display an error or return to the confirmation view
                var couponResponse = await _couponService.GetCouponByIdAsync(couponId, ct);
                return View("Delete", couponResponse.Value);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
